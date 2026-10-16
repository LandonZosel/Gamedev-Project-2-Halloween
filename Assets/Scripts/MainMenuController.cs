using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, ISaveSystem
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private SaveSystem saveSystem;
    GameData data;
    public void PlayButton()
    {
        SceneManager.LoadScene("Shop");
    }

    public void NewGameButton()
    {
        data = new GameData();
        saveSystem.SaveGame();
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        if (!(data.dayCount > 0))
        {
            continueButton.interactable = false;
        }

        this.data = data;
    }

    public void SaveData(ref GameData data)
    {
        data = this.data;
    }
}
