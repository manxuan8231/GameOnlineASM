using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineCamera FollowCamera; // Camera chính
    public float rotationSpeed = 150f;     // Tốc độ xoay
    public float verticalLimit = 80f;      // Giới hạn góc xoay lên xuống

    private Transform playerTransform;     // Thân player để xoay ngang
    private Transform cameraPivot;         // Điểm để xoay lên xuống
    private float verticalAngle = 0f;      // Góc xoay dọc

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AssignCamera(Transform player)
    {
        playerTransform = player;

        // Tạo pivot gắn vào đầu player
        cameraPivot = new GameObject("CameraPivot").transform;
        cameraPivot.SetParent(playerTransform);
        cameraPivot.localPosition = new Vector3(0, 1.6f, 0f); // cao khoảng đầu người

        // Gán camera vào pivot
        FollowCamera.transform.SetParent(cameraPivot);
        FollowCamera.transform.localPosition = Vector3.zero;
        FollowCamera.transform.localRotation = Quaternion.identity;

        FollowCamera.Follow = cameraPivot;
        FollowCamera.LookAt = cameraPivot;
    }

    private void LateUpdate()
    {
        if (playerTransform == null || cameraPivot == null) return;

        // Xoay ngang (player thân người)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        playerTransform.Rotate(Vector3.up, mouseX);

        // Xoay dọc (chỉ camera pivot)
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        verticalAngle -= mouseY;
        verticalAngle = Mathf.Clamp(verticalAngle, -verticalLimit, verticalLimit);
        cameraPivot.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);

        // Toggle chuột
        if (Input.GetKeyDown(KeyCode.L))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !locked;
        }
    }
}
