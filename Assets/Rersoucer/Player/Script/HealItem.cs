using UnityEngine;

public class HealItem : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip healSound;
    public float healAmout = 50f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(healSound);
            HpSlider playerHp = other.GetComponent<HpSlider>();
            playerHp.heal(healAmout);
            Debug.Log("Heal");
            Destroy(gameObject);
        }
    }
}
