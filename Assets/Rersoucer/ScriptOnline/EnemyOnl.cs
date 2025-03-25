using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOnl : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public GameObject[] tagerts;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        tagerts = GameObject.FindGameObjectsWithTag("Player");
        if (tagerts.Length == 0) return;

        //tim nguoi choi gan nhat
        GameObject target = null;
        float minDistance = Mathf.Infinity;
        foreach (var item in tagerts)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = item;
            }
        }
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
