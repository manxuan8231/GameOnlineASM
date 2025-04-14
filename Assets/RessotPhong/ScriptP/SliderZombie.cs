using Fusion;
using UnityEngine;

public class SliderZombie : NetworkBehaviour
{
    public int CurrentHealth;
    public int MaxHealth = 100;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }


    public void TakeDamage(int damage, PlayerProperties attacker)
    {
        CurrentHealth -= damage;
        animator.SetTrigger("Hit");

        if (CurrentHealth <= 0)
        {
            if (attacker != null && attacker.Object.HasInputAuthority)
            {
                attacker.AddEnemyKill();
            }

            Runner.Despawn(Object);
        }
    }


}
