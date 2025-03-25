using UnityEngine;

public class Repair : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
  
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Car car = FindAnyObjectByType<Car>();
            car.GetWheel(1);
            Destroy(gameObject);
            audioSource.PlayOneShot(clip);
            
        }
    }
}
