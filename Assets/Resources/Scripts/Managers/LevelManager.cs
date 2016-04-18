using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public Text levelText;
    int level;
    float multiplyer = 1.5f;

    void Start()
    {
        GetSetLevelText();
    }

    void GetSetLevelText()
    {
        level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 1;
        PlayerPrefs.SetInt("Level", level);
        levelText.text = "Level: " + level.ToString();
    }

    void Update()
    {
        if (!EnemyManager.instance.AreThereEnemiesLeft())
            SaveStatsAndOpenUpgradeShop();
    }

    void SaveStatsAndOpenUpgradeShop()
    {
        if (Player.instance.health > 0)
        {
            Debug.Log("Klopt niet");
            PlayerPrefs.SetInt("EnemyHealth", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyHealth") * multiplyer));
            PlayerPrefs.SetInt("EnemyDamage", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyDamage") * multiplyer));
            PlayerPrefs.SetInt("HealthyDrop", Mathf.RoundToInt(PlayerPrefs.GetInt("HealthyDrop") * multiplyer));
            PlayerPrefs.SetInt("MoneyDrop", Mathf.RoundToInt(PlayerPrefs.GetInt("MoneyDrop") * multiplyer));
            PlayerPrefs.SetInt("ScoreDrop", Mathf.RoundToInt(PlayerPrefs.GetInt("ScoreDrop") * multiplyer));
            PlayerPrefs.SetInt("MaxEnemySpawn", Mathf.RoundToInt(PlayerPrefs.GetInt("MaxEnemySpawn") * multiplyer));
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("CurrentMoney", Player.instance.money);
            PlayerPrefs.SetInt("CurrentScore", Player.instance.score);
        }

        UpgradeShop.instance.StartUpgradeShop();
        Destroy(GetComponent<LevelManager>());
    }
}