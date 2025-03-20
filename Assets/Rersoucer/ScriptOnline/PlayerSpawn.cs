using Fusion;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefabs; // Chứa danh sách nhân vật có thể spawn

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
           
            Vector3 spawnPosition = new Vector3(-30.4f, 0, -237.8f);

            Runner.Spawn(playerPrefabs, spawnPosition, Quaternion.identity, Runner.LocalPlayer, (runner, obj) =>
            {
                var playerSetup = obj.GetComponent<PlayerSetup>();
                if (playerSetup != null)
                {
                    playerSetup.SetupCamera();
                    playerSetup.SetupPlayer();
                }
            });
        }
    }
}
