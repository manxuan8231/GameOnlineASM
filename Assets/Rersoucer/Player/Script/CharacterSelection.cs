using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private PlayerSpawn playerSpawn;

    void Start()
    {
        playerSpawn = FindAnyObjectByType<PlayerSpawn>();
    }
    void Update()
    {
        SelectCharacter();
    }
    public void SelectCharacter()
    {
       
           playerSpawn.characterIndex = 1;
           
        
    }
    public void SelectCharacter2()
    {
        playerSpawn.characterIndex = 2;

    }
}
