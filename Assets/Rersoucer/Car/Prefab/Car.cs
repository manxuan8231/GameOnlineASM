using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnRepairChanged))]

    public float countRepair { get; set; }
    public TextMeshProUGUI textRepair;
    public GameObject buttonF;
    public void OnRepairChanged()
    {
        textRepair.text = $"{countRepair}/{4}";
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonF.SetActive(true);
            PlayerProperties playerProperties = FindAnyObjectByType<PlayerProperties>();
            if (Input.GetKeyDown(KeyCode.F) && playerProperties.countRepair > 0)
            {
                countRepair += 1;
                textRepair.text = $"{countRepair}/{4}";
                countRepair = Mathf.Clamp(countRepair, 0, 4);
            }
        }
        else
        {
            buttonF.SetActive(false);
        }
    }
    public override void Spawned()
    {
        base.Spawned();
        OnRepairChanged();

    }
    public void Start()
    {
        countRepair = 0;
        textRepair.text = $"{countRepair}/{4}";
        buttonF.SetActive(false);
    }
}
