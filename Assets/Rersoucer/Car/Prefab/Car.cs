using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnRepairChanged))]
    public float countRepair { get; set; }

    public TextMeshProUGUI textRepair;
    public TextMeshProUGUI textButtonF;
    public NetworkRunner networkRunner;
    private bool isNearPlayer = false;
    private Transform TransformCar;
    

    public void OnRepairChanged()
    {
        textRepair.text = $"{countRepair}%";
    }

    private void Update()
    {
        
        if (!Object.HasInputAuthority) return;

        if (isNearPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Gửi RPC tới host để tăng sửa
                RPC_AddRepair();
            }
        }
    }
    private void Spawn()
    {
        if (networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            TransformCar = GameObject.FindGameObjectWithTag("Car").transform;
            var car = networkRunner.Spawn(gameObject, TransformCar.position, transform.rotation);
            car.GetComponent<NetworkObject>().GetComponent<Car>().countRepair = 0;
        }
        else
        {
            textRepair.text = $"{countRepair}%";
            textButtonF.enabled = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<NetworkObject>().HasInputAuthority)
        {
            isNearPlayer = true;
            textButtonF.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<NetworkObject>().HasInputAuthority)
        {
            isNearPlayer = false;
            textButtonF.enabled = false;
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddRepair()
    {
        countRepair = Mathf.Clamp(countRepair + 1, 0, 100);
    }
    public void Start()
    {
        Spawn();
    }
}

