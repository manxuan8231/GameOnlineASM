using UnityEngine;

public class ButtonF : MonoBehaviour
{ 
    void Start()
    {
        
    }
  
    void Update()
    {
        Car car = FindAnyObjectByType<Car>(); //goi ham       
        if (Input.GetKeyDown(KeyCode.F) && car.wheelCount >= 1)
        {
            car.TruWheel(1);
            Wheel wheel = FindAnyObjectByType<Wheel>();
            wheel.wheel.SetActive(true);           
        }
    }
}
