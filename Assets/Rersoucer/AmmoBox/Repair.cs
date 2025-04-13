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
       
    }
}
