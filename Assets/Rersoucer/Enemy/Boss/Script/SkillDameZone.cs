using UnityEngine;

public class SkillDameZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 45;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HpSlider playerHp = other.GetComponent<HpSlider>();
            playerHp.TakeDame(damage);
        }
    }
}
