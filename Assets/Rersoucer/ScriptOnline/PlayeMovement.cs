using Fusion;
using UnityEngine;

public class PlayeMovement : NetworkBehaviour
{
    public CharacterController controller;  // Điều khiển nhân vật
    public float speed = 12f;
    public float jumpHeight = 3f;   // Độ cao khi nhảy
    public float gravity = -9.81f;  // Trọng lực
    private float yVelocity;        // Tốc độ rơi
    public Transform groundCheck;   // Điểm kiểm tra tiếp đất
    public float groundDistance = 0.2f; // Khoảng cách kiểm tra tiếp đất
    public LayerMask groundMask;    // Lớp mặt đất
    public override void FixedUpdateNetwork()
    {
        // Kiểm tra quyền điều khiển nhân vật
        if (!Object.HasStateAuthority) return;

        // Kiểm tra xem nhân vật có đang đứng trên mặt đất không
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; // Đặt lại vận tốc rơi khi chạm đất
        }

        // Nhận input từ bàn phím
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.fixedDeltaTime);

        // Tính toán vận tốc để cập nhật Animator
        PlayerProperties playerProperties = GetComponent<PlayerProperties>();
        playerProperties.speed = move.magnitude; //magnitude: độ lớn của vector


        // Xử lý nhảy
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);        
        }

        // Áp dụng trọng lực
        yVelocity += gravity * Time.fixedDeltaTime;
        controller.Move(Vector3.up * yVelocity * Time.fixedDeltaTime);


    }
}
