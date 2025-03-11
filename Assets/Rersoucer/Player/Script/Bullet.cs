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
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            bloodEffect.SetActive(true);
            Destroy(gameObject,2f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            bloodEffect.SetActive(false);
           
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            bloodEffect.SetActive(false);

        }
    }
}
