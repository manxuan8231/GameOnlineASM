using UnityEngine;

public class SliderZombie : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth = 100;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }

    
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        animator.SetTrigger("Hit");
        Debug.Log(CurrentHealth);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
