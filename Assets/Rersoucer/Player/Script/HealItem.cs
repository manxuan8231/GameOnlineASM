using UnityEngine;

public class HealItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float healAmout = 50f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HpSlider playerHp = other.GetComponent<HpSlider>();
            playerHp.heal(healAmout);
            Debug.Log("Heal");
            Destroy(gameObject);
        }
    }
}
