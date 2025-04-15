using Fusion;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.TextCore;

public class Fly : NetworkBehaviour
{
    public Transform victoryPo;
    public GameObject victoryPrefab;
    public CinemachineCamera cinemachineCamera;
    void Start()
    {
        StartCoroutine(WaitForSeconds(3f)); // Đợi 3 giây trước khi bắt đầu
    }

    
    void Update()
    {
       
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MainManager mainManager = FindAnyObjectByType<MainManager>();

            if (mainManager._runner != null && mainManager._runner.LocalPlayer.IsRealPlayer)
            {
                victoryPo = GameObject.FindGameObjectWithTag("Fly").transform;
                var fly = mainManager._runner.Spawn(victoryPrefab, victoryPo.position, Quaternion.identity);
               
                Time.timeScale = 0; // Dừng thời gian
            }
        }
    }
    IEnumerator WaitForSeconds(float seconds)
    {
        cinemachineCamera.Priority = 100; // Đặt độ ưu tiên cho camera
        yield return new WaitForSeconds(seconds);
        cinemachineCamera.Priority = 0; // Đặt độ ưu tiên cho camera
    }
}
