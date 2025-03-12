using UnityEngine;

public class Wheel3 : MonoBehaviour
{
    public GameObject wheel3;
    public GameObject buttonF3;
    void Start()
    {
        buttonF3.SetActive(false);
        wheel3.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF3.SetActive(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF3.SetActive(false);

        }
    }
}
