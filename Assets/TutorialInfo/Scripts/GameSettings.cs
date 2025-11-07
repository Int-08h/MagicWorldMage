using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    // Тип игрового режима
    public string gameMode = ""; // "Campaign", "Survival", "Workshop", "Spells"

    // Для режимов с картами
    public string selectedMap = "";
    public int selectedDifficulty = 1; // 1–5

    // Прогресс (пример)
    public int spellPoints = 0;
    public int workshopPoints = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
