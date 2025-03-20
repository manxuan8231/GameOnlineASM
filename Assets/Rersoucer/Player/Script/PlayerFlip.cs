using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Lấy vị trí chuột trên màn hình và chuyển sang tọa độ thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Đảm bảo không ảnh hưởng đến trục Z (nếu game 2D)

        // Tính hướng từ Player đến chuột
        Vector3 direction = mousePosition - transform.position;

        // Tính góc xoay (đổi sang độ)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Áp dụng góc quay cho nhân vật
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
