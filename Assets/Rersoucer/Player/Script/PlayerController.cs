using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class PlayerController : NetworkBehaviour
{
    private Animator _animator;
    public float moveJump = 10;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform targetTransform;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;

    public Transform cameraTransform;

    private float nextFireTime = 0f;
    private bool isFiring = false;
    private bool isReloading = false; // Biến kiểm tra có đang nạp đạn không
    private Rigidbody rb;

    public GameObject effectHit;

    public int ammoCount = 45;
    public int maxAmmo = 45;
    public TextMeshProUGUI ammoText;

    public GameObject bangDan;

    public float cooldownJump = 2f;
    private float timeJump = 0f;
    void Start()
    {
        bangDan.SetActive(true);
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        UpdateAmmoUI();
    }

    public override void FixedUpdateNetwork()
    {
        if (!isReloading) // Chỉ cho phép di chuyển nếu không nạp đạn
        {
            Move();
        }
        Firing();
        Jump();
        Aim();

        if (Input.GetKeyDown(KeyCode.R) && ammoCount < maxAmmo) // Chỉ nạp đạn khi chưa đầy đạn
        {
            Reload();
        }
    }

    void Move()
    {
        if (cameraTransform == null || isFiring || isReloading) return; // Không di chuyển khi đang bắn hoặc nạp đạn

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 worldMoveDirection = (camForward * vertical + camRight * horizontal).normalized;
            transform.Translate(worldMoveDirection * currentSpeed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(worldMoveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _animator.SetBool("isWalk", !isRunning);
            _animator.SetBool("isRun", isRunning);
        }
        else
        {
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isReloading && Time.time >= timeJump + cooldownJump) // Không cho nhảy khi đang nạp đạn
        {
            _animator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * moveJump, ForceMode.Impulse);
            timeJump = Time.time;
        }
    }

    void Aim()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

        }
    }

    void Firing()
    {
        if (ammoCount > 0 && Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime && !isReloading) // Không bắn khi đang nạp đạn
        {
            isFiring = true;
            _animator.SetBool("isFire", true);
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);

            if (cameraTransform != null)
            {
                Vector3 lookDirection = cameraTransform.forward;
                lookDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            isFiring = false;
            _animator.SetBool("isFire", false);
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Destroy(bullet, 3f);
            GameObject effect = Instantiate(effectHit, firePoint.position, Quaternion.identity);
            Destroy(effect, 0.2f);

            if (rb != null)
            {
                Vector3 shootDirection = targetTransform != null
                    ? (targetTransform.position - firePoint.position).normalized
                    : firePoint.forward;

                rb.linearVelocity = shootDirection * bulletSpeed;
            }

            ammoCount--;
            UpdateAmmoUI();
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{ammoCount}/{maxAmmo}";
        }
    }

    void Reload()
    {
        if (isReloading) return; // Nếu đang nạp đạn thì không gọi lại
        isReloading = true; // Bắt đầu nạp đạn, khóa di chuyển
        _animator.SetTrigger("Reload");
    }

    public void StartReload()
    {
        isReloading = true;
        bangDan.SetActive(false);
    }

    public void EndReload()
    {
        isReloading = false;
        ammoCount = maxAmmo; // Nạp đầy đạn
        bangDan.SetActive(true);
        UpdateAmmoUI();
    }
}