using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float detectionRadius = 20f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int damage = 5;
    public AudioClip attackSound;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private bool isAttacking;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FindTarget();
        if (target != null && !isAttacking)
        {
            MoveTowardsTarget();
        }
    }

    void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance && distance <= detectionRadius)
            {
                minDistance = distance;
                closestPlayer = player.transform;
            }
        }
        target = closestPlayer;
    }

    void MoveTowardsTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
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
        animator.SetTrigger("hitdame");
        audioSource.PlayOneShot(attackSound);
        yield return new WaitForSeconds(0.5f);

        if (target != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        nextAttackTime = Time.time + attackCooldown;
    }
}