using UnityEngine;

public class Bullet : MonoBehaviour
{
    SliderZombie sliderZombie;
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
            sliderZombie = other.gameObject.GetComponent<SliderZombie>();
            if (sliderZombie != null)
            {
                sliderZombie.TakeDamage(40);
                Destroy(gameObject);
            }                    
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController boss = FindAnyObjectByType<BossController>();    
            boss.TakeDamage(10);
            Debug.Log("đã chạm");
            Destroy(gameObject);
        }
    }
    
}
