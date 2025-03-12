using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bloodEffect;
    void Start()
    {
        bloodEffect.SetActive(false);
    }
    
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            bloodEffect.SetActive(true);
            
            Destroy(gameObject,2f);
            Debug.Log("đã chạm");
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            bloodEffect.SetActive(true);
            Destroy(gameObject,2f);
        }
    }
    
}
