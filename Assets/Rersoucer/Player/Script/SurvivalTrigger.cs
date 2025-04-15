using UnityEngine;
using TMPro;
using System.Collections;

public class SurvivalTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText;   // Text để hiển thị thông báo mục tiêu
    public TextMeshProUGUI timerText;     // Text để hiển thị thời gian đếm ngược
    public float countdownTime = 600f;    // 10 phút
    private bool timerStarted = false;

    void Start()
    {
        // Khi scene load, bắt đầu luôn
        if (!timerStarted)
        {
            timerStarted = true;
            Debug.Log("Bắt đầu game - Đếm ngược sinh tồn!");

            // Hiện thông báo mục tiêu
            messageText.text = "Mục tiêu: Tìm cách sống sót và chờ cứu trợ.";
            messageText.gameObject.SetActive(true);

            // Bắt đầu đếm ngược
            StartCoroutine(CountdownTimer());
        }
    }

    IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(3f); // Hiện mục tiêu trong 3 giây rồi ẩn
        messageText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);

        float timeLeft = countdownTime;
        while (timeLeft > 0)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        timerText.text = "00:00";
        timerText.gameObject.SetActive(false);

        // Gọi hàm xử lý khi hết giờ
        OnTimerFinished();
    }

    void OnTimerFinished()
    {
        // TODO: Viết hành động bạn muốn ở đây khi hết giờ
        Debug.Log(">> Hết thời gian! Thực hiện hành động sau sinh tồn.");
    }
}
