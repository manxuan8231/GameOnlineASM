using UnityEngine;

public class DameZone : MonoBehaviour
{
    public int damage = 15;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HpSlider playerHp = other.GetComponent<HpSlider>();
            playerHp.TakeDame(damage);    
        }
    }
}
