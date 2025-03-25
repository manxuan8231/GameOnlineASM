using Fusion;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerGun : NetworkBehaviour
{
    // Đối tượng đạn và điểm bắn
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Tham chiếu đến NetworkRunner
    public NetworkRunner networkRunner;

    // Biến tổng số đạn và đạn hiện tại
    [SerializeField] private int maxAmmo = 45;
    private int currentAmmo;

    // UI để hiển thị số lượng đạn
    public TextMeshProUGUI ammoText;
    public GameObject imgBullet;

    // Thời gian gài đạn
    public float reloadTime = 2f;
    private bool isReloading = false;

    private void Start()
    {
        if (!Object.HasInputAuthority)
        {
            if (ammoText != null)
                ammoText.enabled = false; // Ẩn UI với người chơi khác
            return;
        }

        // Khởi tạo số đạn
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    private void Update()
    {
        // Nếu không phải người chơi sở hữu, không xử lý
        if (!Object.HasInputAuthority || isReloading) return;

        // Bắn khi nhấn chuột hoặc phím F
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.F)) && currentAmmo > 0)
        {
            Shoot();
        }
        // Gài đạn khi nhấn R
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        // Tìm mục tiêu có tag "Tam"
        GameObject target = GameObject.FindGameObjectWithTag("Tam");

        if (target != null && networkRunner != null && networkRunner.LocalPlayer.IsRealPlayer)
        {
            // Tạo viên đạn và hướng bắn
            var bullet = networkRunner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().AddForce(direction * 50f, ForceMode.Impulse);

            // Giảm đạn và cập nhật UI
            currentAmmo--;
            UpdateAmmoText();

            // Kiểm tra hết đạn và tự động gài lại
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        if (isReloading) yield break;  // Đang gài đạn thì không làm gì 
        isReloading = true;
        ammoText.text = $"{currentAmmo}/45..."; // Hiển thị trạng thái gài đạn

        yield return new WaitForSeconds(reloadTime);  // Chờ trong thời gian gài đạn

        currentAmmo = maxAmmo;
        UpdateAmmoText();

        isReloading = false;
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null && Object.HasInputAuthority)
        {
            // Cập nhật hiển thị số đạn
            ammoText.text = $"{currentAmmo}/{maxAmmo}";
            imgBullet.SetActive(true);
        }
        else
        {
            imgBullet.SetActive(false);
        }
    }
}
