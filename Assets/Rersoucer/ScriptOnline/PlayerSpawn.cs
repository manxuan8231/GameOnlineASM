using Fusion;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    public int characterIndex = 0;
    public void PlayerJoined(PlayerRef player)
    {
        
            if (player == Runner.LocalPlayer)
            {
                Vector3 position = new Vector3(-30.41528f, -1.907349e-06f, -237.8279f);

                Runner.Spawn(playerPrefab, position, Quaternion.identity, Runner.LocalPlayer, (runner, obj) =>
                {
                    var playerSetup = obj.GetComponent<PlayerSetup>();
                    if (playerSetup != null)
                    {
                        playerSetup.SetupCamera();
                        playerSetup.SetupPlayer();
                    }
                });
            }
        if (player == Runner.LocalPlayer)
        {
            Vector3 position = new Vector3(-30.41528f, -1.907349e-06f, -237.8279f);

            Runner.Spawn(playerPrefab2, position, Quaternion.identity, Runner.LocalPlayer, (runner, obj) =>
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
    public void PlayerJoined2(PlayerRef player)
    {
        if (characterIndex == 2)
        {
            
        }
    }
}
