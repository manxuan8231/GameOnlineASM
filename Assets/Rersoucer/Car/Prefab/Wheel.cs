using UnityEngine;

public class Wheel : MonoBehaviour
{
    public GameObject wheel;
    public GameObject buttonF;
    void Start()
    {
        buttonF.SetActive(false);
        wheel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF.SetActive(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF.SetActive(false);

        }
    }
}
