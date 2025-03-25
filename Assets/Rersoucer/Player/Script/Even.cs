using Fusion;
using System.Collections;
using TMPro;
using UnityEngine;

public class Even : MonoBehaviour
{
    public Transform[] spawnEnemy;
    public GameObject enemy;

    public Transform[] spawnEnemy2;
    public GameObject enemy2;

    public Transform[] spawnEnemy3;
    public GameObject enemy3;

    public Transform[] spawnEnemy4;
    public GameObject enemy4;
    public NetworkRunner networkRunner;

    private bool offspawn = true;
    private bool offspawn2 = true;
    private bool offspawn3 = true;
    private bool offspawn4 = true;

    public TextMeshProUGUI textStep;

    public GameObject boxRepair2;
    public GameObject boxRepair3;
    public GameObject boxRepair4;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {       
        textStep.enabled = false;
        boxRepair2.SetActive(false);
        boxRepair3.SetActive(false);
        boxRepair4.SetActive(false);
       
    }   
    void Update()
    {
        if(offspawn)
        {
            Debug.Log("spawn dot 1");
            StartCoroutine(SpawnEnemy1());
            offspawn = false;
        }
        Car car = FindAnyObjectByType<Car>();
        if (car.wheelCount >= 1 && offspawn2)
        {
            Debug.Log("spawn dot 2");
            StartCoroutine(SpawnEnemy2());
            offspawn2 = false;
            offspawn3 = true;
        }
        if (car.wheelCount >= 2 && offspawn3)
        {
            Debug.Log("spawn dot 3");
            StartCoroutine(SpawnEnemy3());
            offspawn3 = false;
            offspawn4 = true;
        }
        if (car.wheelCount >= 3 && offspawn4)
        {
            Debug.Log("spawn dot 4");
            StartCoroutine(SpawnEnemy4());
            offspawn4 = false;
        }
    }
    public IEnumerator SpawnEnemy1()
    {
        yield return new WaitForSeconds(2);
        textStep.enabled = true;
        textStep.text = "Wave 1";
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy)
        {
             networkRunner.Spawn(enemy, item.position, Quaternion.identity);
        }
    }
    public IEnumerator SpawnEnemy2()
    {
        yield return new WaitForSeconds(20);
        textStep.enabled = true;
        textStep.text = "Wave 2";
        boxRepair2.SetActive(true);
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy2)
        {
            networkRunner.Spawn(enemy2, item.position, Quaternion.identity);
        }
    }
    public IEnumerator SpawnEnemy3()
    {
        yield return new WaitForSeconds(20);
        textStep.enabled = true;
        textStep.text = "Wave 3";
        boxRepair3.SetActive(true);
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy3)
        {
            networkRunner.Spawn(enemy3, item.position, Quaternion.identity);
        }
    }
    public IEnumerator SpawnEnemy4()
    {
        yield return new WaitForSeconds(20);
        textStep.enabled = true;
        textStep.text = "Mission";
       audioSource.PlayOneShot(audioClip);
        boxRepair4.SetActive(true);
        yield return new WaitForSeconds(5);
        textStep.enabled = false;
        foreach (var item in spawnEnemy4)
        {
            networkRunner.Spawn(enemy4, item.position, Quaternion.identity);
        }
    }
}
