using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    SliderZombie sliderZombie;
    private float time = 0f;
    public NetworkRunner runner;
    public NetworkObject networkObject;

    public override void FixedUpdateNetwork()
    {
        time += Runner.DeltaTime;
        if (time >= 2) {
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

            }
            if (runner != null && networkObject != null)
                runner.Despawn(networkObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController boss = FindAnyObjectByType<BossController>();
            if (sliderZombie != null)
            {
                boss.TakeDamage(10);
                Debug.Log("đã chạm");
            }
            if (runner != null && networkObject != null)
                runner.Despawn(networkObject);
        }
        
    }
    private void Start()
    {
        if (networkObject == null)
            networkObject = GetComponent<NetworkObject>();

        if (runner == null)
            runner = FindAnyObjectByType<NetworkRunner>();
    }

}
