﻿using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : NetworkBehaviour
{
    public GameObject wheel;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject can;

    public int wheelCount = 0;
    public int maxWheelCount = 4;

    public TextMeshProUGUI wheelText;

    public Slider sliderRepair;
    public TextMeshProUGUI textRepair;

    public GameObject PanelVictory;
    void Start()
    {
        PanelVictory.SetActive(false);
        sliderRepair.maxValue = 100;
        sliderRepair.value = 0;
        textRepair.text = $"{sliderRepair.value}%";
        wheelText.text = $"{wheelCount}/{maxWheelCount}";
    }


    public override void FixedUpdateNetwork()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GetWheel(1);
        }
       
        if (wheel.activeSelf && wheel1.activeSelf && wheel2.activeSelf && wheel3.activeSelf)//điều kiện win
        {
            can.SetActive(false);//cai can
            //ghi chức năng ở đây 
            PanelVictory.SetActive(true);
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
