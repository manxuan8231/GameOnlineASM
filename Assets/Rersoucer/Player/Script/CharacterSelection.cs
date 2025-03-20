using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static int selectedCharacter = 0; // Mặc định nhân vật 1

    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        Debug.Log("Nhân vật được chọn: " + selectedCharacter);
        SceneManager.LoadScene("Map2"); // Chuyển sang scene game
        PlayerPrefs.Save();
    }
}
