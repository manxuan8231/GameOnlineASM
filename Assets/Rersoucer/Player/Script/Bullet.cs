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
            Debug.Log("đã chạm");
        }
        else if (other.gameObject.CompareTag("Boss"))
        {           
            Destroy(gameObject);
        }
    }
    
}
