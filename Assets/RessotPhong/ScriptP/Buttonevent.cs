using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenu;

    // Hàm này sẽ được gọi khi nhấn nút để tải scene
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Hàm này sẽ được gọi khi nhấn nút để mở phần setting bị ẩn và ẩn Main Menu
    public void OpenSettings()
    {
        
            settingsPanel.SetActive(true);
            mainMenu.SetActive(false);
        
    }
    public void Back()
    {
        settingsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Hàm này sẽ được gọi khi nhấn nút để thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}