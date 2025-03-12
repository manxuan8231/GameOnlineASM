using UnityEngine;

public class ButtonF2 : MonoBehaviour
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
            Wheel2 wheel2 = FindAnyObjectByType<Wheel2>();
            wheel2.wheel2.SetActive(true);           
        }
    }
}
