using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panele Menu")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;

    [Header("Strzałki Nawigacji")]
    [SerializeField] private GameObject mainMenuArrow;
    [SerializeField] private GameObject levelSelectArrow;

    [Header("Ustawienia Scen")]
    [SerializeField] private string gameSceneName = "Level 1";

    [Header("Skrypty Tekstu Głośności")]
    [SerializeField] private VolumeText soundVolumeScript;
    [SerializeField] private VolumeText musicVolumeScript;

    private void Start()
    {
        ShowMainMenuPanel();
    }

    public void ShowMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);

        mainMenuArrow.SetActive(true);
        levelSelectArrow.SetActive(false);
    }

    public void ShowLevelSelectPanel()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);

        mainMenuArrow.SetActive(false);
        levelSelectArrow.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadLevelByIndex(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeSound()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.ChangeSoundVolume(0.2f);
        
        if (soundVolumeScript != null)
            soundVolumeScript.RefreshText();
    }

    public void ChangeMusic()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.ChangeMusicVolume(0.2f);

        if (musicVolumeScript != null)
            musicVolumeScript.RefreshText();
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}