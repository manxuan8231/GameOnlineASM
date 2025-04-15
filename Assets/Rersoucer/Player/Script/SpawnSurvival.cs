using Fusion;
using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnSurvival : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bossSpawn;
    public Transform bossPo;
    public NetworkRunner networkRunner;
    
    public bool isReloading = false;

    private void Update()
    {
        if (!Object.HasInputAuthority) return;

        // Giữ chuột trái hoặc phím Q để bắn liên tục
        if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            Shoot();
        }       
    }

    private void Shoot()
    {
        if (networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            Vector3 firePoint = Vector3.zero; // Thay thế bằng vị trí thực tế của firePoint
            var sv = networkRunner.Spawn(bulletPrefab, firePoint, Quaternion.identity);
            bossPo = GameObject.FindGameObjectWithTag("Car").transform;
            var boss = networkRunner.Spawn(bossSpawn, bossPo.position, Quaternion.Euler(0f, 180f, 0f)
);
        }
    }
}
