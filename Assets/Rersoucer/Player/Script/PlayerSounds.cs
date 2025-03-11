using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clipWalk;
    public AudioClip clipRun;
    public AudioClip clipJump;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public void Walk()
    {
        audioSource.PlayOneShot(clipWalk);
    }
    public void Run()
    {
        audioSource.PlayOneShot(clipRun);
    }
    public void Jump()
    {
        audioSource.PlayOneShot(clipJump);
    }
}
