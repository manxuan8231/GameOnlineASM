using UnityEngine;

public class Target : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        // Tìm đối tượng đầu tiên có tag "Tam"
        GameObject targetObj = GameObject.FindGameObjectWithTag("Tam2");
        if (targetObj != null)
        {
            target = targetObj.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối tượng có tag 'Tam'");
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Gán trực tiếp vị trí mỗi frame (di chuyển theo ngay lập tức)
            transform.position = target.position;
        }
    }
}
