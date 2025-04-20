using Fusion;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerGun : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public NetworkRunner networkRunner;

    [SerializeField] private int maxAmmo = 45;
    public int currentAmmo;

    public TextMeshProUGUI ammoText;
    public GameObject imgBullet;

    public float reloadTime = 2f;
    public float fireRate = 0.2f; // Khoảng thời gian giữa mỗi phát bắn (bắn nhanh/slower)

    private bool isReloading = false;
    private float nextFireTime = 0f;

    private void Start()
    {
        if (!Object.HasInputAuthority)
        {
            if (ammoText != null)
                ammoText.enabled = false;
            return;
        }

        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    private void Update()
    {
        if (!Object.HasInputAuthority || isReloading) return;

        // Giữ chuột trái hoặc phím Q để bắn liên tục
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Q)) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
            PlayerProperties playerProperties = FindAnyObjectByType<PlayerProperties>();
            if (playerProperties != null)
            {
                playerProperties.UseMana(1);
            }
        }

        // Gài đạn khi nhấn R
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Tam");

        if (target != null && networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            var bullet = networkRunner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);

            //  Gắn shooter vào viên đạn
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.shooter = GetComponent<PlayerProperties>();

            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().AddForce(direction * 120f, ForceMode.Impulse);

            currentAmmo--;
            UpdateAmmoText();

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }


    private IEnumerator Reload()
    {
        if (isReloading) yield break;

        isReloading = true;
        ammoText.text = $"{currentAmmo}/{maxAmmo}...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        UpdateAmmoText();

        isReloading = false;
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null && Object.HasInputAuthority)
        {
            ammoText.text = $"{currentAmmo}/{maxAmmo}";
            imgBullet?.SetActive(true);
        }
        else
        {
            imgBullet?.SetActive(false);
        }
    }
}
