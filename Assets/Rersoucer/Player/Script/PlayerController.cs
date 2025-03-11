using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    public float moveJump = 10;
    public float walkSpeed = 5f; // Tốc độ đi bộ
    public float runSpeed = 10f; // Tốc độ chạy
    public float rotationSpeed = 10f; // Tốc độ xoay nhân vật

    public GameObject bulletPrefab; // Prefab của đạn
    public Transform firePoint; // Vị trí bắn 
    public Transform targetTransform; // Transform của mục tiêu 
    public float bulletSpeed = 20f; // Tốc độ bay của đạn
    public float fireRate = 0.2f; // Thời gian delay giữa các lần bắn

    public Transform cameraTransform; // Transform của Camera
    
    private float nextFireTime = 0f; // Thời gian tiếp theo có thể bắn

    private bool isFiring = false; // Biến kiểm tra trạng thái bắn

    private Rigidbody rb;

    public GameObject effectHit;

   
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Firing();
        Jump();
        Aim();
    }

    void Move()
    {
        if (cameraTransform == null || isFiring) return; // Không di chuyển khi đang bắn

        float horizontal = Input.GetAxis("Horizontal"); // A, D hoặc phím mũi tên Trái/Phải
        float vertical = Input.GetAxis("Vertical"); // W, S hoặc phím mũi tên Lên/Xuống
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // Kiểm tra có giữ Shift không

        float currentSpeed = isRunning ? runSpeed : walkSpeed; // Nếu giữ Shift thì chạy
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0) // Nếu có nhập di chuyển
        {
            // Xác định hướng di chuyển theo Camera
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0; // Loại bỏ độ nghiêng của Camera
            camRight.y = 0;

            Vector3 worldMoveDirection = (camForward * vertical + camRight * horizontal).normalized;

            // Di chuyển nhân vật
            transform.Translate(worldMoveDirection * currentSpeed * Time.deltaTime, Space.World);

            // Quay nhân vật theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(worldMoveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Xác định animation đi bộ hay chạy
            _animator.SetBool("isWalk", !isRunning);
            _animator.SetBool("isRun", isRunning);
        }
        else
        {
            // Dừng animation
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * moveJump, ForceMode.Impulse);
        }
    }

    void Aim()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {

        }
    }
    void Firing()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime) // Kiểm tra nếu có thể bắn
        {
            isFiring = true; // Đánh dấu đang bắn
            _animator.SetBool("isFire", true);
            // Dừng animation
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
            // Xoay nhân vật về phía Camera
            if (cameraTransform != null)
            {
                Vector3 lookDirection = cameraTransform.forward;
                lookDirection.y = 0; // Loại bỏ độ nghiêng
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            ShootBullet();
            nextFireTime = Time.time + fireRate; // Đặt thời gian delay cho lần bắn tiếp theo
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            isFiring = false; // Cho phép di chuyển lại khi ngừng bắn
            _animator.SetBool("isFire", false);
        }
    }
    void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);        
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            GameObject effect = Instantiate(effectHit, firePoint.position, Quaternion.identity);
            Destroy(effect, 0.2f);
            if (rb != null)
            {
                Vector3 shootDirection;

                // Nếu có mục tiêu, bắn đến vị trí của mục tiêu
                if (targetTransform != null)
                {
                    shootDirection = (targetTransform.position - firePoint.position).normalized;
                }
                else
                {
                    // Nếu không có target, bắn thẳng về phía trước
                    shootDirection = firePoint.forward;
                }

                rb.linearVelocity = shootDirection * bulletSpeed; // Đạn bay về phía target
            }
        }
    }
}
