using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    SliderZombie sliderZombie;
    private float time = 0f;
    public NetworkRunner runner;
    public NetworkObject networkObject;
    public GameObject effectHit;
    public override void FixedUpdateNetwork()
    {
        time += Runner.DeltaTime;
        if (time >= 10) {
            if (runner != null && networkObject != null)
                runner.Despawn(networkObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            sliderZombie = other.gameObject.GetComponent<SliderZombie>();
            if (sliderZombie != null)
            {
                sliderZombie.TakeDamage(40);
                var effect = runner.Spawn(effectHit, transform.position, transform.rotation);
            }
            if (runner != null && networkObject != null)
                runner.Despawn(networkObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController boss = FindAnyObjectByType<BossController>();
           
                //boss.TakeDamage(0);
                var effect = runner.Spawn(effectHit, transform.position, transform.rotation);

                Debug.Log("đã chạm");
            
            if (runner != null && networkObject != null)
                runner.Despawn(networkObject);
        }
        
    }
    public void SpawnEffect()
    {
       
    }
    private void Start()
    {
        if (networkObject == null)
            networkObject = GetComponent<NetworkObject>();

        if (runner == null)
            runner = FindAnyObjectByType<NetworkRunner>();
    }

}
