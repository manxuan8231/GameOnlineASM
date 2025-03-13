using System.Collections;
using TMPro;
using UnityEngine;

public class Even : MonoBehaviour
{
    public Transform[] spawnEnemy;
    public GameObject enemy;

    public Transform[] spawnEnemy2;
    public GameObject enemy2;
    
    private bool offspawn = true;
    private bool offspawn2 = true;

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
        Car car = FindAnyObjectByType<Car>();
        if (car.wheelCount == 1 && offspawn2)
        {
            StartCoroutine(SpawnEnemy2());
            offspawn2 = false;
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
    public IEnumerator SpawnEnemy2()
    {
        yield return new WaitForSeconds(20);
        textStep.enabled = true;
        textStep.text = "Wave 2";
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy2)
        {
            Instantiate(enemy2, item.position, Quaternion.identity);
        }
    }
}
