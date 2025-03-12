using UnityEngine;

public class Wheel2 : MonoBehaviour
{
    public GameObject wheel2;
    public GameObject buttonF2;
    void Start()
    {
        buttonF2.SetActive(false);
        wheel2.SetActive(false);
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF2.SetActive(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF2.SetActive(false);

        }
    }
}
