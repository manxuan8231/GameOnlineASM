using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in " + gameObject.name);
            return;
        }

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}