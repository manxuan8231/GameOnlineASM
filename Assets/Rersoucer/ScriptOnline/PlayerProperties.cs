using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
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

    [Networked, OnChangedRender(nameof(OnManaChanged))]
    public float currentMana { get; set; }
    public float maxMana { get; set; }

    public TextMeshProUGUI textMana;
    public UnityEngine.UI.Slider manaSlider;
    public GameObject sliderMana;

    public void OnManaChanged()
    {
        if (Object.HasInputAuthority)
        {
            textMana.text = $"{currentMana}/{maxMana}";
            manaSlider.value = currentMana;
            sliderMana.SetActive(true);
        }
        else
        {
            sliderMana.SetActive(false);
        }
    }
    public void GetHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
    public void GetMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
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
        else if(other.gameObject.CompareTag("Hp"))
        {
            currentHealth += 40;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            currentMana += 40;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        }
       
       
    }
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        textHealth.text = $"{currentHealth}/{maxHealth}";
        maxMana = 100;
        currentMana = maxMana;
        textMana.text = $"{currentMana}/{maxMana}";

        textEnemy.text = $"{countEnemy}";
    }



    [Networked, OnChangedRender(nameof(OnPlayerInfoChanged))]
    public PlayerNetworkInfo playerInfo { get; set; }
    public void OnPlayerInfoChanged()
    {
       
    }


}
