using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenuController : MonoBehaviour, ISaveSystem
{
    [SerializeField]
    private SaveSystem saveSystem;
    GameData data;

    public void StartButton()
    {
        data = new GameData();
        saveSystem.SaveGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadData(GameData data)
    {
        this.data = data;
    }

    public void SaveData(ref GameData data)
    {
        data = this.data;
    }
}
