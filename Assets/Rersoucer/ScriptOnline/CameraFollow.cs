using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineCamera FollowCamera;   // Camera chính
    public float rotationSpeedX = 150f;        // Tốc độ xoay
    public float rotationSpeedY = 150f;        // Tốc độ xoay
    public float verticalLimit = 80f;      // Giới hạn góc nhìn lên/xuống (tối đa 80 độ)

    private Transform playerTransform; // Thân người chơi
    private float verticalAngle = 0f; // Góc xoay dọc X

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AssignCamera(Transform player)
    {
        playerTransform = player;

        if (FollowCamera != null && playerTransform != null)
        {
            // Gán camera vào player (camera đứng ở đầu)
            FollowCamera.transform.SetParent(playerTransform);
            FollowCamera.transform.localPosition = new Vector3(0, 2, 0);
            FollowCamera.transform.localRotation = Quaternion.identity;

            // Tắt Follow/LookAt để điều khiển thủ công
            FollowCamera.Follow = null;
            FollowCamera.LookAt = null;
        }
    }

    private void LateUpdate()
    {
        if (playerTransform == null || FollowCamera == null) return;

        // Xoay thân người chơi trái phải theo chuột
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
        playerTransform.Rotate(Vector3.up, mouseX);

        // Xoay camera lên xuống
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeedY * Time.deltaTime;
        verticalAngle -= mouseY;  // Giảm để lật hướng đúng
        verticalAngle = Mathf.Clamp(verticalAngle, -verticalLimit, verticalLimit); // Giới hạn góc
        FollowCamera.transform.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);

        // Toggle chuột bằng phím L
        if (Input.GetKeyDown(KeyCode.L))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !locked;
        }
    }
}
