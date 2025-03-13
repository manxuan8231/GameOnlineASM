using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            
            Destroy(gameObject);
           
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController boss = FindAnyObjectByType<BossController>();    
            boss.TakeDamage(20);
            Debug.Log("đã chạm");
            Destroy(gameObject);
        }
    }
    
}
