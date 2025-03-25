using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Fusion;

public class ZombieAI2 : NetworkBehaviour
{
    public float detectionRadius = 20f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int damage = 5;
    public AudioClip attackSound;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private bool isAttacking;
    private float nextAttackTime;

    [Networked] private NetworkObject target { get; set; } // Đồng bộ mục tiêu
    [Networked] private Vector3 targetPosition { get; set; } // Đồng bộ vị trí mục tiêu

    public override void Spawned()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (Object.HasStateAuthority) // Chỉ server hoặc người sở hữu chạy AI
        {
            InvokeRepeating(nameof(FindTarget), 1f, 1f);
        }
    }

    void Update()
    {
        if (target != null && !isAttacking)
        {
            MoveTowardsTarget();
        }
    }

    void FindTarget()
    {
        if (!Object.HasStateAuthority) return; // Chỉ thực hiện trên chủ sở hữu

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        NetworkObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance && distance <= detectionRadius)
            {
                minDistance = distance;
                closestPlayer = player.GetComponent<NetworkObject>();
            }
        }
        target = closestPlayer;
        if (target != null)
        {
            targetPosition = target.transform.position;
        }
    }

    void MoveTowardsTarget()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPosition);
            animator.SetFloat("speed", 1);
        }
        else
        {
            agent.isStopped = true;
            animator.SetFloat("speed", 0);
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        RPC_DealDamage();
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        nextAttackTime = Time.time + attackCooldown;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_DealDamage()
    {
        if (target != null)
        {
            HpSlider playerHealth = target.GetComponent<HpSlider>();
            if (playerHealth != null)
            {
                playerHealth.TakeDame(damage);
                audioSource.PlayOneShot(attackSound);
            }
        }
    }
}
