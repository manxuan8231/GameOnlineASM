using Fusion;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject bossHiden;
    public NetworkRunner networkRunner;
     void Start()
    {
        if(networkRunner.IsServer)
        {
            bossHiden.SetActive(false);
        }
        
    }
}
