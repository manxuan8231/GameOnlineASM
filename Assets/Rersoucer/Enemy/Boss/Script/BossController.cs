﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController
    : MonoBehaviour
{
    public enum EnemyState { Dancing, Combat, IdleCombat, Walk, Death, Skill }
    public EnemyState currentState;

    private Transform player;
    public float radius = 25f;
    public float rangeAttack = 2f;
    public float rangeSkill = 5f;
    public float cooldownSkill = 15f;

    public GameObject FloatingTargetPrefab;
    public GameObject skill;
    public GameObject boxDamage;
    public GameObject skillDameZone;

    [SerializeField] private string targetTag = "Player";
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip dancingClip;
    public AudioClip combatClip;
    public AudioClip skillClip;
    public AudioClip deathClip;

    public int maxHealth = 200;
    public int currentHealth;

    private bool hasDancing = false;
    private bool isAttacking = false;
    public bool canUseSkill = true;

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
    }

    void Update()
    {
        if (currentState == EnemyState.Death) return;

        player = FindNearestPlayer();
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

    Transform FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(targetTag);
        if (players.Length == 0) return null;

        Transform nearest = null;
        float minDistance = Mathf.Infinity;
        foreach (var p in players)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = p.transform;
            }
        }
        return nearest;
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
                if (!audioSource.isPlaying) audioSource.PlayOneShot(dancingClip);
                break;

            case EnemyState.IdleCombat:
                agent.isStopped = true;
                break;

            case EnemyState.Walk:
                agent.isStopped = false;
                agent.speed = 3.5f;
                agent.SetDestination(player.position);

                if (distanceToTarget <= rangeAttack)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    StartCoroutine(AttackRoutine());
                }
                else if (distanceToTarget <= rangeSkill && canUseSkill)
                {
                    StartCoroutine(SkillRoutine());
                }
                break;

            case EnemyState.Combat:
                agent.isStopped = true;
                if (distanceToTarget > rangeAttack)
                {
                    ChangeState(EnemyState.Walk);
                }
                break;

            case EnemyState.Skill:
                agent.isStopped = false;
                agent.stoppingDistance = rangeAttack;
                break;

            case EnemyState.Death:
                agent.isStopped = true;
                Debug.Log("Death");
                animator.SetTrigger("Death");
                break;
        }
    }

    IEnumerator DancingRoutine()
    {
        ChangeState(EnemyState.Dancing);
        yield return new WaitForSeconds(7f);
        ChangeState(EnemyState.Walk);
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        ChangeState(EnemyState.Combat);
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;

        float distanceToTarget = Vector3.Distance(player.position, transform.position);
        if (distanceToTarget <= rangeAttack)
        {
            StartCoroutine(AttackRoutine());
        }
        else
        {
            ChangeState(EnemyState.Walk);
        }
    }

    IEnumerator SkillRoutine()
    {
        canUseSkill = false;
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

        if (currentHealth <= 0)
        {
            ChangeState(EnemyState.Death);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(deathClip);

            agent.enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    public void Popup(float damage)
    {
        var go = Instantiate(FloatingTargetPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
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
