using UnityEngine;
using Fusion;

public class ZombieSpawner : NetworkBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public override void Spawned()
    {
        if (Object.HasStateAuthority) // Chỉ server hoặc host spawn zombie
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        NetworkObject zombie = Runner.Spawn(zombiePrefab, spawnPoint.position, Quaternion.identity);
    }
}
