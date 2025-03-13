using UnityEngine;
using UnityEngine.UI; 

public class HpSlider : MonoBehaviour
{
    public Slider sliderHp; 
    public float maxHp = 100f;
    public Slider sliderMana;
    public float maxMana = 100f;
    private float timeSinceStopRunning = 0f; // Thời gian kể từ khi dừng chạy
    void Start()
    {      
        sliderHp.maxValue = maxHp; // Đặt giá trị tối đa cho Slider
        sliderMana.maxValue = maxMana;
    }
    public void TakeDame(float amout)
    {
        sliderHp.value -= amout;
        sliderHp.value = Mathf.Clamp(sliderHp.value, 0, maxHp); 
        if (sliderHp.value <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            sliderMana.value -= 5 * Time.deltaTime;
            timeSinceStopRunning = 0f;
        }
        else
        {
            timeSinceStopRunning += Time.deltaTime;
            if (timeSinceStopRunning >= 1f) // Sau 1 giây thì bắt đầu hồi mana
            {
                sliderMana.value += 20 * Time.deltaTime;
            }
        }
        // Giới hạn mana trong khoảng từ 0 đến maxMana
        sliderMana.value = Mathf.Clamp(sliderMana.value, 0, maxMana);
    }
}
