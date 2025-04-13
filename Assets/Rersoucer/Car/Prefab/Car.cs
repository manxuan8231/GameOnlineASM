using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnRepairChanged))]

    public float countRepair { get; set; }
    public TextMeshProUGUI textRepair;

    public void OnRepairChanged()
    {
        textRepair.text = $"{countRepair}/{4}";
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }
    public void Start()
    {
        countRepair = 0;
        textRepair.text = $"{countRepair}/{4}";
    }
}
