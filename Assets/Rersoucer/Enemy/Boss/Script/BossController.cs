using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public enum EnemyState { Dancing, Combat, IdleCombat, Walk, Death,Skill }
    public EnemyState currentState;
    private Transform player;
    public float radius = 25f;
    public float rangeAttack = 2f;

    [SerializeField] private string targetTag = "";
    private NavMeshAgent agent;
    private Animator animator;
    private bool hasDancing = false;
    private bool isAttacking = false;
    private 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = EnemyState.IdleCombat;

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

        float distanceToTarget = Vector3.Distance(player.position, transform.position);

        if (distanceToTarget <= radius && !hasDancing)
        {
            StartCoroutine(DancingRoutine());
            hasDancing = true;
            return;
        }

        HandleState(distanceToTarget);
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
        animator.ResetTrigger("Death");
        animator.ResetTrigger("Combat");
        animator.ResetTrigger("IdleCombat");
        animator.ResetTrigger("Dancing");

        currentState = newState;
        animator.SetTrigger(newState.ToString());
    }

    void HandleState(float distanceToTarget)
    {
        switch (currentState)
        {
            case EnemyState.Dancing:
                agent.isStopped = true;
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
                break;

            case EnemyState.Combat:
                agent.isStopped = true;

                // Nếu player chạy ra khỏi rangeAttack -> Chuyển về Walk
                if (distanceToTarget > rangeAttack)
                {
                    ChangeState(EnemyState.Walk);
                }
                break;
            case EnemyState.Skill:
                agent.isStopped = false;
                break;
            case EnemyState.Death:
                agent.isStopped = true;
                Destroy(gameObject, 2f);
                break;
           
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
}
