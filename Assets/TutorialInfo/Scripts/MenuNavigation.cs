using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // === MainMenu панели (оставляем для совместимости) ===
    public GameObject panel_MainMenu;
    public GameObject panel_Options;
    public GameObject panel_Story;

    // === Временные значения для MapSelect ===
    private string tempSelectedMap = "";
    private int tempSelectedDifficulty = 1;

    private AudioSource persistentAudio;
    private float mainMusicTime;

    private void Start()
    {
        var dontDestroyObj = DontDestroyManager.Instance?.gameObject;
        if (dontDestroyObj != null)
        {
            persistentAudio = dontDestroyObj.GetComponent<AudioSource>();
        }
    }

    // =============== ГЛАВНОЕ МЕНЮ ===============
    public void OnPlayButton() => SceneManager.LoadScene("ModeSelect");
    public void OnOptionsButton()
    {
        panel_MainMenu.SetActive(false);
        panel_Options.SetActive(true);
    }
    public void OnStoryButton()
    {
        if (persistentAudio != null && persistentAudio.isPlaying)
        {
            mainMusicTime = persistentAudio.time;
            persistentAudio.Pause();
        }
        panel_MainMenu.SetActive(false);
        panel_Story.SetActive(true);
    }
    public void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnBackFromOptions()
    {
        panel_Options.SetActive(false);
        panel_MainMenu.SetActive(true);
    }
    public void OnBackFromStory()
    {
        panel_Story.SetActive(false);
        panel_MainMenu.SetActive(true);
        if (persistentAudio != null)
        {
            persistentAudio.time = mainMusicTime;
            persistentAudio.Play();
        }
    }

    // =============== MODE SELECT ===============
    public void OnSelectCampaign()
    {
        GameSettings.Instance.gameMode = "Campaign";
        SceneManager.LoadScene("MapSelect");
    }

    public void OnSelectSurvival()
    {
        GameSettings.Instance.gameMode = "Survival";
        SceneManager.LoadScene("MapSelect");
    }

    public void OnOpenWorkshop()
    {
        GameSettings.Instance.gameMode = "Workshop";
        SceneManager.LoadScene("WorkshopScene");
    }

    public void OnOpenSpells()
    {
        GameSettings.Instance.gameMode = "Spells";
        SceneManager.LoadScene("SpellsScene");
    }

    public void OnBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // =============== MAP SELECT ===============
    public void OnSelectMap(string mapName)
    {
        tempSelectedMap = mapName;
        Debug.Log("Выбрана карта: " + mapName);
    }

    public void OnSelectDifficulty(int level)
    {
        tempSelectedDifficulty = level;
        Debug.Log("Выбрана сложность: " + level);
    }

    public void OnConfirmMapAndDifficulty()
    {
        if (string.IsNullOrEmpty(tempSelectedMap))
        {
            Debug.LogWarning("Карта не выбрана!");
            return;
        }

        GameSettings.Instance.selectedMap = tempSelectedMap;
        GameSettings.Instance.selectedDifficulty = tempSelectedDifficulty;

        SceneManager.LoadScene("CharacterSelect");
    }
    public void OnBackToModeSelect()
    {
        SceneManager.LoadScene("ModeSelect");
    }
}
