using UnityEngine;
using UnityEngine.UI; 

public class HpSlider : MonoBehaviour
{
    public Slider sliderHp; 
    public float maxHp = 100f;

    void Start()
    {      
        sliderHp.maxValue = maxHp; // Đặt giá trị tối đa cho Slider        
    }
    public void TakeDame(float amout)
    {
        sliderHp.value -= amout;
        if(sliderHp.value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
