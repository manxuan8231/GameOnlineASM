using UnityEngine;

public class Wheel1 : MonoBehaviour
{
    public GameObject wheel1;
    public GameObject buttonF1;
    void Start()
    {
        buttonF1.SetActive(false);
        wheel1.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF1.SetActive(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF1.SetActive(false);

        }
    }
}
