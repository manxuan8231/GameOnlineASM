using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    //khi vao mang thi se tao nhan vat cho nguoi choi
    //public Vector3 d = new (0, 1, 0);
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer) 
        {
            //tạo vị trí ở 0, 1, 0
           var position = new Vector3(0, 1, 0);
            //spawn nhân vật
           Runner.Spawn(playerPrefab, position, Quaternion.identity,
           Runner.LocalPlayer,
           (runner, obj) =>
           {
               var playerSetup = obj.GetComponent<PlayerSetup>();
               if (playerSetup != null) 
               {
                   playerSetup.SetupCamera();//camera                 
                   playerSetup.SetupPlayer();//diem, mp, hp
               };
               var playerGun = obj.GetComponent<PlayerGun>();
               if (playerGun != null)
               {
                   playerGun.networkRunner = runner;
               }
               var playerProperties = obj.GetComponent<PlayerProperties>();
               if(playerProperties != null)
               {
                   playerProperties.networkRunner = runner;
                   playerProperties.networkObject = obj;
               }
           }); 
        }
    }
}

/*ASM 1:
 - PlayerSpawn: tạo đối tượng trong môi trường mạng
 - AssignCamera: gán camera, player, gun cho người chơi
- Properties: animation, movement
- Attack, item,....
 */


