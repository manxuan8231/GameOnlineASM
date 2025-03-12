using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;

    public int wheelCount = 0;
    public int maxWheelCount = 4;

    public TextMeshProUGUI wheelText;

    public GameObject buttonF;
    void Start()
    {
        buttonF.SetActive(false);
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
        wheel1.SetActive(false); 
        wheel2.SetActive(false);
        wheel3.SetActive(false);
        wheel4.SetActive(false);
    }

   
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GetWheel(1);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF.SetActive(true);
            if (Input.GetKeyUp(KeyCode.F) && wheelCount == 1)
            {
                wheel1.SetActive(true);
            }else
            if (Input.GetKeyUp(KeyCode.F) && wheelCount == 2)
            {
                wheel2.SetActive(true);
            }else
            if (Input.GetKeyUp(KeyCode.F) && wheelCount == 3)
            {
                wheel3.SetActive(true);
            }else
            if (Input.GetKeyUp(KeyCode.F) && wheelCount == 4)
            {
                wheel4.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonF.SetActive(false);
        }
    }
    public void GetWheel(int amount)
    {      
        wheelCount += amount;
        wheelCount = Mathf.Clamp(wheelCount, 0, maxWheelCount);
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
    } 
}
