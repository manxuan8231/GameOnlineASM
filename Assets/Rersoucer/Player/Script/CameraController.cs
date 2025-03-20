using Fusion;
using UnityEngine;
using Unity.Cinemachine;
public class CameraController : MonoBehaviour
{
    //SerializeField] private Transform target; 
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 2, -2.5f); // Vị trí camera từ Inspector
    [SerializeField] private float sensitivity = 2f; // Độ nhạy xoay camera
    [SerializeField] private Vector2 rotationLimits = new Vector2(-30f, 60f); // Giới hạn góc nhìn lên/xuống
    [SerializeField] private float smoothSpeed = 10f; // Độ mượt khi xoay camera

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isCursorLocked = true; // Trạng thái khóa con trỏ chuột
    private float defaultZOffset; // Lưu giá trị Z gốc của camera

   public CinemachineCamera followCamera;
    
    void Start()
    {
        LockCursor(true); // Mặc định khóa chuột khi vào game
        defaultZOffset = cameraOffset.z; // Lưu giá trị Z gốc
    }

    void Update()
    {
        HandleCursorToggle();
        HandleZoom();
        RotateCamera();
    }


    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Bấm cùng lúc Alt + L
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

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
    }

    public void FollowPlayer(Transform targetPL)
    {
        
        if (followCamera != null && targetPL != null)
        {
            followCamera.Follow = targetPL; // Không Follow trực tiếp
            followCamera.LookAt = targetPL; // Camera luôn nhìn về nhân vật
        }

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Vector3 desiredPosition = targetPL.position + rotation * cameraOffset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothSpeed); // Làm mượt xoay


        // Đặt vị trí camera quanh nhân vật
        followCamera.transform.position = targetPL.position + cameraOffset;
        followCamera.transform.LookAt(targetPL);
    }
}
