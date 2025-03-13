using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    
    public enum EnemyState { Dancing, Combat, IdleCombat, Walk, Death,Skill }
    public EnemyState currentState;
    private Transform player;
    public float radius = 25f;
    public float rangeAttack = 2f;
    public GameObject FloatingTargetPrefab;
    [SerializeField] private string targetTag = "";
    private NavMeshAgent agent;
    private Animator animator;
    private bool hasDancing = false;
    private bool isAttacking = false;
    public float rangeSkill = 5f;

    public float cooldownSkill = 15f;
    public bool canUseSkill = true;
    public int maxHealth = 200;
    public int currentHealth;

    private AudioSource audioSource;
    public AudioClip dancingClip;
    public AudioClip combatClip;
    public AudioClip skillClip;
    public AudioClip deathClip;
    public GameObject skill;
    public GameObject boxDamage;
    public GameObject skillDameZone;
    void Start()
    {
        skillDameZone.SetActive(false);
        boxDamage.SetActive(false);
        skill.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = EnemyState.IdleCombat;
        currentHealth = maxHealth;
        GameObject playerObject = GameObject.FindGameObjectWithTag(targetTag);
        if (playerObject != null)
            player = playerObject.transform;
        else
            Debug.LogError($"Không tìm thấy đối tượng nào có tag: {targetTag}");
    }

    void Update()
    {
        if (currentState == EnemyState.Death) return;
        if (player == null) return;
        if (audioSource == null) return;

        float distanceToTarget = Vector3.Distance(player.position, transform.position);

        if (distanceToTarget <= radius && !hasDancing)
        {
            StartCoroutine(DancingRoutine());
            hasDancing = true;
            return;
        }
       
        HandleState(distanceToTarget);
    }
    IEnumerator showSKillRoutie()
    {
        skill.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        skill.SetActive(false);
    }
    public void ShowSkill()
    {
        StartCoroutine(showSKillRoutie());
    }
    public void SkillSoundANMT()
    {
        if (currentState == EnemyState.Skill)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(skillClip);
            }
        }
    }

    IEnumerator DancingRoutine()
    {
        ChangeState(EnemyState.Dancing);
        yield return new WaitForSeconds(7f); // Nhảy trong 7 giây
        ChangeState(EnemyState.Walk);
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        animator.ResetTrigger("Walk");
        
        animator.ResetTrigger("Combat");
        animator.ResetTrigger("IdleCombat");
        animator.ResetTrigger("Dancing");
        animator.ResetTrigger("Skill");
        currentState = newState;
        animator.SetTrigger(newState.ToString());
    }

    void HandleState(float distanceToTarget)
    {
        switch (currentState)
        {
            case EnemyState.Dancing:
                agent.isStopped = true;
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(dancingClip);
                }
                break;

            case EnemyState.IdleCombat:
                agent.isStopped = true;
                break;

            case EnemyState.Walk:
                agent.isStopped = false;
                agent.speed = 3.5f;
                agent.SetDestination(player.position);

                // Khi vào rangeAttack -> Stop di chuyển & Attack
                if (distanceToTarget <= rangeAttack)
                {
                    agent.isStopped = true; // Ngăn boss di chuyển khi tấn công
                    agent.ResetPath(); // Dừng SetDestination
                    StartCoroutine(AttackRoutine());
                }
                else if (distanceToTarget <= rangeSkill && canUseSkill)
                {
                    StartCoroutine(SkillRoutie());
                }
                break;

            case EnemyState.Combat:
                agent.isStopped = true;
                //if (!audioSource.isPlaying)
                //{
                //    audioSource.PlayOneShot(combatClip);
                //}
                // Nếu player chạy ra khỏi rangeAttack -> Chuyển về Walk
                if (distanceToTarget > rangeAttack)
                {
                    ChangeState(EnemyState.Walk);
                }
                break;
            case EnemyState.Skill:
                agent.isStopped = true;
                agent.stoppingDistance = rangeAttack;

                break;
            case EnemyState.Death:
                agent.isStopped = true;
                Debug.Log("Death");

                animator.SetTrigger("Death");
                    //if (!audioSource.isPlaying)
                    //{
                    //    audioSource.PlayOneShot(deathClip);
                    //}
                    //agent.enabled = false; // Vô hiệu hóa AI tránh lỗi
                    //GetComponent<Collider>().enabled = false;
                    //Destroy(gameObject, 2f);   
                break;
            
           
        }
    }
    public void combatSoundAnmt()
    {
        if(currentState == EnemyState.Combat)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(combatClip);
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        ChangeState(EnemyState.Combat);
        yield return new WaitForSeconds(1.5f); // Giả lập thời gian đánh
        isAttacking = false;

        float distanceToTarget = Vector3.Distance(player.position, transform.position);

        // Nếu player vẫn trong rangeAttack -> tiếp tục Combat
        if (distanceToTarget <= rangeAttack)
        {
            StartCoroutine(AttackRoutine());
        }
        else
        {
            ChangeState(EnemyState.Walk);
        }
    }
        IEnumerator SkillRoutie()
    {
        canUseSkill = true;
        ChangeState(EnemyState.Skill);
        yield return new WaitForSeconds(3f);
        ChangeState(EnemyState.Walk);
        StartCoroutine(SkillCooldownRoutine());
    }
    IEnumerator SkillCooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownSkill);
        canUseSkill = true; 
    }

    public void TakeDamage(int damage)
    {
        if (currentState == EnemyState.Death) return;
        currentHealth -= damage;
        Debug.Log(currentHealth);
        Popup(damage);
        
    

        if(currentHealth <= 0)
        {
            ChangeState(EnemyState.Death);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(deathClip);
            }
            agent.enabled = false; // Vô hiệu hóa AI tránh lỗi
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2f);
        }

    }
    public void Popup(float damage)
    {
        // hiện popup
        var go = Instantiate(FloatingTargetPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
        Debug.Log(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeSkill);
    }

    public void StartDame()
    {
        boxDamage.SetActive(true);
    }
    public void EndDame()
    {
        boxDamage.SetActive(false);
    }

    public void StartSkill()
    {
        skillDameZone.SetActive(true);
    }
    public void EndSkill()
    {
        skillDameZone.SetActive(false);
    }
}
