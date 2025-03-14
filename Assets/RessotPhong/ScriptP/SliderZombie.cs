using UnityEngine;

public class SliderZombie : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth = 100;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
