using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Player cần follow
    public Vector3 cameraOffset = new Vector3(0, 2, -2.5f); // Điều chỉnh vị trí camera từ Inspector
    public float sensitivity = 2f; // Độ nhạy xoay camera
    public Vector2 rotationLimits = new Vector2(-30f, 60f); // Giới hạn góc nhìn lên/xuống
    public float smoothSpeed = 10f; // Độ mượt khi xoay camera

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isCursorLocked = true; // Trạng thái khóa con trỏ chuột
    private float defaultZOffset; // Lưu giá trị Z gốc của camera
    void Start()
    {
        LockCursor(true); // Mặc định khóa chuột khi vào game
        defaultZOffset = cameraOffset.z; // Lưu giá trị Z gốc
    }

    void Update()
    {
        HandleCursorToggle(); //alt
        HandleZoom(); // Kiểm tra zoom khi nhấn chuột phải
        RotateCamera(); // Xoay camera theo chuột
        FollowPlayer(); // Camera follow nhân vật
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftAlt))
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
        if (Input.GetKey(KeyCode.Mouse1)) // Nếu nhấn chuột phải
        {
            cameraOffset.z = -2;
        }
        else
        {
            cameraOffset.z = defaultZOffset; // Reset về giá trị gốc khi thả chuột
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, rotationLimits.x, rotationLimits.y); // Giới hạn góc nhìn lên/xuống
        rotationY += mouseX;
    }

    void FollowPlayer()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Vector3 desiredPosition = target.position + rotation * cameraOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position); // Camera luôn nhìn về Player
    }
}
