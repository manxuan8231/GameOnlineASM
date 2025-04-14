using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))] 
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }

    public TextMeshProUGUI textHealth;  
    public UnityEngine.UI.Slider healthSlider;
    public GameObject sliderHp;
    public NetworkObject networkObject;
    public NetworkRunner networkRunner;


    public void OnHealthChanged()
    {
        if (Object.HasInputAuthority)
        {
            textHealth.text = $"{currentHealth}/{maxHealth}";
            healthSlider.value = currentHealth;
            sliderHp.SetActive(true);
        }
        else
        {
           sliderHp.SetActive (false);
        }    
    }
    public void GetHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    [Networked, OnChangedRender(nameof(OnSpeedChanged))]
   
    public float speed { get; set; }
    public Animator animator;
    public int speedHash = Animator.StringToHash("Speed");
    public void OnSpeedChanged()
    {
        animator.SetFloat(speedHash, speed);
    }

    [Networked, OnChangedRender(nameof(OnChangeName))]
    public string Name { get; set; }

    public TextMeshProUGUI nameText;

    private void OnChangeName()
    {
        nameText.text = Name;
    }

    public override void Spawned()
    {
        base.Spawned();
        OnChangeName();
        // khởi tạo thông số cho người chơi
        if (Object.HasStateAuthority)
        {         
            var name = PlayerPrefs.GetString("PlayerName", "Player");
            RpcUpdateName(name);
        }
    }
    // hàm này sẽ được gọi khi người chơi nhập tên
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcUpdateName(string name)
    {
        Name = name;
    }

    [Networked, OnChangedRender(nameof(OnChangeKillEnemy))]
     public int countEnemy { get; set; }
    public TextMeshProUGUI textEnemy;
    public GameObject panelEnemy;
    public void OnChangeKillEnemy()
    {
        if (Object.HasInputAuthority)
        {
            panelEnemy.SetActive(true);
            textEnemy.text = $"{countEnemy}";
        }
        else
        {
            panelEnemy.SetActive(false);
        }
    }
    public void AddEnemyKill()
    {
        countEnemy += 1;
        // Khi countEnemy thay đổi, OnChangeKillEnemy sẽ tự gọi
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Boss"))
        {
            currentHealth -= 10;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth <= 0)
            {
                //destroy(GameObject) ko xai
                networkRunner.Despawn(networkObject);
            }
        }
       
    }
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        textHealth.text = $"{currentHealth}/{maxHealth}";

        textEnemy.text = $"{countEnemy}";
    }



    [Networked, OnChangedRender(nameof(OnPlayerInfoChanged))]
    public PlayerNetworkInfo playerInfo { get; set; }
    public void OnPlayerInfoChanged()
    {
       
    }


}
