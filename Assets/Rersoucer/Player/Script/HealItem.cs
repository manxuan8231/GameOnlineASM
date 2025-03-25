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
           /* HpSlider playerHp = other.GetComponent<HpSlider>();
            playerHp.heal(healAmout);*/
           PlayerProperties playerProperties = FindAnyObjectByType<PlayerProperties>();
            playerProperties.GetHealth(30);
            Debug.Log("Heal");
            Destroy(gameObject);
        }
    }
}
