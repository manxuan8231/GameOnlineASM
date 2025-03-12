using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject wheel;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject can;

    public int wheelCount = 0;
    public int maxWheelCount = 4;

    public TextMeshProUGUI wheelText;

    void Start()
    {      
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
    }

   
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GetWheel(1);
        }
        if(wheel.activeSelf && wheel1.activeSelf && wheel2.activeSelf && wheel3.activeSelf)
        {
            can.SetActive(false);
        }
    }
    public void GetWheel(int amount)
    {      
        wheelCount += amount;
        wheelCount = Mathf.Clamp(wheelCount, 0, maxWheelCount);
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
    } 
    public void TruWheel(int amount)
    {
        wheelCount -= amount;
        wheelCount = Mathf.Clamp(wheelCount, 0, maxWheelCount);
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
    }
}
