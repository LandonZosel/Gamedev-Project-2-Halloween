using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, ISaveSystem
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Image volumeIcon;
    [SerializeField]
    private Sprite volumeOn;
    [SerializeField]
    private Sprite volumeMuted;
    private bool isMuted = false;
    [SerializeField]
    private SaveSystem saveSystem;
    GameData data;
    void Awake()
    {
        isMuted = AudioListener.pause;
        volumeIcon.sprite = isMuted ? volumeMuted : volumeOn;
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Shop");
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Story");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void FullScreenButton()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void VolumeButton()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        volumeIcon.sprite = isMuted ? volumeMuted : volumeOn;
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
