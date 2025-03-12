using UnityEngine;

public class ButtonF3 : MonoBehaviour
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
            Wheel3 wheel3 = FindAnyObjectByType<Wheel3>();
            wheel3.wheel3.SetActive(true);           
        }
    }
}
