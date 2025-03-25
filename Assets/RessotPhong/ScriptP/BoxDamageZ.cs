using UnityEngine;

public class ZombieBoxDame : MonoBehaviour
{
    public int damage = 5;
    private bool hasDealtDamage = false; // Tránh gây sát thương nhiều lần trong 1 đòn đánh

    private void OnTriggerEnter(Collider other)
    {
        if (!hasDealtDamage && other.CompareTag("Player"))
        {
            HpSlider playerHp = other.GetComponent<HpSlider>();
            if (playerHp != null)
            {
                playerHp.TakeDame(damage);
                hasDealtDamage = true;
            }
           
        }
    }

    public void ResetDamage()
    {
        hasDealtDamage = false;
    }
}
