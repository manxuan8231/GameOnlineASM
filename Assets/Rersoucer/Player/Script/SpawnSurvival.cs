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

    public Transform[] enemyPoi;
    public GameObject enemyPrefab;

    private void Update()
    {
        if (!Object.HasInputAuthority) return;

        // Giữ chuột trái hoặc phím Q để bắn liên tục
        if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            SpawnTextAndBoss();
            SpawnEnemy();
        }       
    }

    private void SpawnTextAndBoss()
    {
        if (networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            Vector3 firePoint = Vector3.zero; 
            var sv = networkRunner.Spawn(bulletPrefab, firePoint, Quaternion.identity);//spawn text

            bossPo = GameObject.FindGameObjectWithTag("Car").transform;
            var boss = networkRunner.Spawn(bossSpawn, bossPo.position, Quaternion.Euler(0f, 180f, 0f));

        }
    }
    private void SpawnEnemy()
    {
        if (networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            foreach (Transform enemyPo in enemyPoi)
            {
                var enemy = networkRunner.Spawn(enemyPrefab, enemyPo.position, Quaternion.identity);
            }
        }
    }
}
