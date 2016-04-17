using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject scripts;
    public Text levelText;
    int level;
    float multiplyer = 1.5f;

    void Start()
    {
        GetSetLevelText();
    }

    void GetSetLevelText()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        levelText.text = "Level: " + level.ToString();
    }

    void Update()
    {
        if (!AreThereEnemiesLeft())
            SaveStatsAndOpenUpgradeShop();
    }

    bool AreThereEnemiesLeft()
    {
        if (EnemyManager.instance.spawnTime > 0)
            return true;
        else if (EnemyManager.instance.enemies.Count == 0)
            return false;
        else
            return true;
    }

    void SaveStatsAndOpenUpgradeShop()
    {
        PlayerPrefs.SetInt("EnemyHealth", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyHealth") * multiplyer));
        PlayerPrefs.SetInt("EnemyDamage", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyDamage") * multiplyer));
        PlayerPrefs.SetInt("MoneyDrop", Mathf.RoundToInt(PlayerPrefs.GetInt("MoneyDrop") * multiplyer));
        PlayerPrefs.SetInt("MaxEnemySpawn", Mathf.RoundToInt(PlayerPrefs.GetInt("MaxEnemySpawn") * multiplyer));
        PlayerPrefs.SetInt("DropScore", Mathf.RoundToInt(PlayerPrefs.GetInt("DropScore") * multiplyer));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);


        scripts.GetComponent<UpgradeShop>().StartUpgradeShop();
        Destroy(GetComponent<LevelManager>());
    }
}
