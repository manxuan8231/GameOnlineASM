using System.Collections;
using UnityEngine;
using Fusion;

public class SummonPrefabOnSpawn : NetworkBehaviour
{
    public GameObject prefabToSummon; // Prefab cần triệu hồi
    public Transform spawnPoint; // Vị trí xuất hiện của prefab
    private float spawnTime; // Thời gian player xuất hiện

    public override void Spawned()
    {
        if (Object.HasStateAuthority) // Chỉ chủ sở hữu mới thực hiện triệu hồi
        {
            spawnTime = Time.time; // Lưu lại thời gian player vào game
            Debug.Log($"Player spawned at time: {spawnTime:F2} seconds");

            StartCoroutine(SummonAfterDelay(2f)); // Triệu hồi sau 2 giây
        }
    }

    private IEnumerator SummonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (Runner.IsServer || Runner.IsSharedModeMasterClient) // Chỉ Server/MasterClient mới tạo Network Object
        {
            Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position + Vector3.forward * 2; // Vị trí spawn
            Runner.Spawn(prefabToSummon, spawnPosition, Quaternion.identity);

            Debug.Log($"Prefab spawned at position: {spawnPosition} (Time: {Time.time:F2} seconds)");
        }
    }
}
