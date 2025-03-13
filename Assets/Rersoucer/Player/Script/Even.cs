using System.Collections;
using TMPro;
using UnityEngine;

public class Even : MonoBehaviour
{
    public Transform[] spawnEnemy;
    public GameObject enemy;

    private bool offspawn = true;

    
    public TextMeshProUGUI textStep;
    void Start()
    {
      textStep.enabled = true;
    }

   
    void Update()
    {
        if(offspawn)
        {
            StartCoroutine(SpawnEnemy1());
            offspawn = false;
        }   
    }
    public IEnumerator SpawnEnemy1()
    {
        textStep.enabled = true;
        textStep.text = "Wave 1";
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy)
        {
             Instantiate(enemy, item.position, Quaternion.identity);
        }
    }
}
