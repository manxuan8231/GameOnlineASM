using Fusion;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 2, -2.5f); // Vị trí camera từ Inspector
    [SerializeField] private float sensitivity = 2f; // Độ nhạy xoay camera
    [SerializeField] private Vector2 rotationLimits = new Vector2(-30f, 60f); // Giới hạn góc nhìn lên/xuống
    [SerializeField] private float smoothSpeed = 10f; // Độ mượt khi xoay camera

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isCursorLocked = true; // Trạng thái khóa con trỏ chuột
    private float defaultZOffset; // Lưu giá trị Z gốc của camera

    void Start()
    {
        LockCursor(true); // Mặc định khóa chuột khi vào game
        defaultZOffset = cameraOffset.z; // Lưu giá trị Z gốc
    }

    void Update() // Dùng Update để tránh giật hình khi xoay
    {
        if (!Object.HasStateAuthority) return; // Chỉ xử lý camera của người chơi cục bộ

        HandleCursorToggle();
        HandleZoom();
        RotateCamera();
    }

    void LateUpdate() // Dùng LateUpdate để tránh jitter khi follow
    {
        FollowPlayer();
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.LeftAlt)) // Bấm cùng lúc Alt + L
        {
            isCursorLocked = !isCursorLocked;
            LockCursor(isCursorLocked);
        }
    }

    void LockCursor(bool lockCursor)
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    void HandleZoom()
    {
        cameraOffset.z = Input.GetKey(KeyCode.Mouse1) ? -2 : defaultZOffset;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, rotationLimits.x, rotationLimits.y);
        rotationY += mouseX;
    }

    void FollowPlayer()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Vector3 desiredPosition = target.position + rotation * cameraOffset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothSpeed); // Làm mượt xoay
    }
}
