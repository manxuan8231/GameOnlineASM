using UnityEngine;

public class ButtonF1 : MonoBehaviour
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
            Wheel1 wheel1 = FindAnyObjectByType<Wheel1>();
            wheel1.wheel1.SetActive(true);           
        }
    }
}
