using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class HpSlider : NetworkBehaviour
{

    [Networked, OnChangedRender(nameof(OnHealthChanged))]
   
    public float currentHp { get; set; }
    public float maxHp { get; set; }
    public float currentMana { get; set; }
    public float maxMana { get; set; }
    private float timeSinceStopRunning = 0f; // Thời gian kể từ khi dừng chạy

    public Slider sliderHp;
    public Slider sliderMana;

    void Start()
    {
        maxHp = 100;
        currentHp = maxHp;
        sliderHp.maxValue = currentHp; // Đặt giá trị tối đa cho Slider

        maxMana = 100;
        currentMana = maxMana;
        sliderMana.maxValue = currentMana;
    }
    public void OnHealthChanged()
    {
        sliderHp.maxValue = maxHp; // Đặt giá trị tối đa cho Slider
        sliderMana.maxValue = maxMana;
    }
    public void TakeDame(float amout)
    {
        currentHp -= amout;
       sliderHp.value = Mathf.Clamp(currentHp, 0, maxHp); 
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public override void FixedUpdateNetwork()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentMana -= 5 * Time.deltaTime;
            timeSinceStopRunning = 0f;
        }
        else
        {
            timeSinceStopRunning += Time.deltaTime;
            if (timeSinceStopRunning >= 1f) // Sau 1 giây thì bắt đầu hồi mana
            {
                currentMana += 20 * Time.deltaTime;
            }
        }
        // Giới hạn mana trong khoảng từ 0 đến maxMana
        sliderMana.value = Mathf.Clamp(currentMana, 0, maxMana);
    }
    public void heal(float amout)
    {
        currentHp += amout;
        sliderHp.value = Mathf.Clamp(currentHp, 0, maxHp);
    }
}
