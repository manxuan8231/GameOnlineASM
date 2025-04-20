using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }

    public TextMeshProUGUI textHealth;
    public Slider healthSlider;
    public GameObject sliderHp;

    [Networked, OnChangedRender(nameof(OnManaChanged))]
    public float currentMana { get; set; }
    public float maxMana { get; set; }

    public TextMeshProUGUI textMana;
    public Slider manaSlider;
    public GameObject sliderMana;

    [Networked, OnChangedRender(nameof(OnSpeedChanged))]
    public float speed { get; set; }

    public Animator animator;
    public int speedHash = Animator.StringToHash("Speed");

    [Networked, OnChangedRender(nameof(OnChangeName))]
    public string Name { get; set; }
    public TextMeshProUGUI nameText;

    [Networked, OnChangedRender(nameof(OnChangeKillEnemy))]
    public int countEnemy { get; set; }
    public TextMeshProUGUI textEnemy;
    public GameObject panelEnemy;

    public NetworkObject networkObject;
    public NetworkRunner networkRunner;

    public override void Spawned()
    {
        base.Spawned();
        OnChangeName();

        // Nếu không phải người điều khiển, ẩn UI đi
        if (!Object.HasInputAuthority)
        {
            sliderHp.SetActive(false);
            sliderMana.SetActive(false);
            panelEnemy.SetActive(false);
            return;
        }

        // Nếu là người chơi điều khiển, đặt tên
        var name = PlayerPrefs.GetString("PlayerName", "Player");
        RpcUpdateName(name);
    }

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
            sliderHp.SetActive(false);
        }
    }

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

    public void OnSpeedChanged()
    {
        if (animator != null)
        {
            animator.SetFloat(speedHash, speed);
        }
    }

    private void OnChangeName()
    {
        nameText.text = Name;
    }

    public void OnChangeKillEnemy()
    {
        if (Object.HasInputAuthority)
        {
            panelEnemy.SetActive(true);
            textEnemy.text = $"{countEnemy}";
        }
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcUpdateName(string name)
    {
        Name = name;
    }

    public void AddEnemyKill()
    {
        countEnemy += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Boss"))
        {
            currentHealth -= 10;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                networkRunner.Despawn(networkObject);
            }
        }
        else if (other.gameObject.CompareTag("Hp"))
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
        maxMana = 100;
        currentMana = maxMana;

        if (Object.HasInputAuthority)
        {
            textHealth.text = $"{currentHealth}/{maxHealth}";
            textMana.text = $"{currentMana}/{maxMana}";
            textEnemy.text = $"{countEnemy}";

            sliderHp.SetActive(true);
            sliderMana.SetActive(true);
            panelEnemy.SetActive(true);
        }
        else
        {
            sliderHp.SetActive(false);
            sliderMana.SetActive(false);
            panelEnemy.SetActive(false);
        }
    }


    [Networked, OnChangedRender(nameof(OnPlayerInfoChanged))]
    public PlayerNetworkInfo playerInfo { get; set; }
    public void OnPlayerInfoChanged()
    {
        // Bạn có thể xử lý thêm khi playerInfo thay đổi nếu cần
    }

    public void UseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
