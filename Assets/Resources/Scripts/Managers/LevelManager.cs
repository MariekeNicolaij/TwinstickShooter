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
        PlayerPrefs.SetInt("PlayerHealth", Mathf.RoundToInt(PlayerPrefs.GetInt("PlayerHealth") * multiplyer));
        PlayerPrefs.SetInt("PlayerMovementSpeed", Mathf.RoundToInt(PlayerPrefs.GetInt("PlayerMovementSpeed") * multiplyer));
        PlayerPrefs.SetInt("EnemyHealth", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyHealth") * multiplyer));
        PlayerPrefs.SetInt("BulletDamage", Mathf.RoundToInt(PlayerPrefs.GetInt("BulletDamage") * multiplyer));
        PlayerPrefs.SetInt("EnemyDamage", Mathf.RoundToInt(PlayerPrefs.GetInt("EnemyDamage") * multiplyer));
        PlayerPrefs.SetInt("MoneyDrop", Mathf.RoundToInt(PlayerPrefs.GetInt("MoneyDrop") * multiplyer));
        PlayerPrefs.SetInt("MaxEnemySpawn", Mathf.RoundToInt(PlayerPrefs.GetInt("MaxEnemySpawn") * multiplyer));
        PlayerPrefs.SetInt("Score", Mathf.RoundToInt(PlayerPrefs.GetInt("Score") * multiplyer));
        PlayerPrefs.SetInt("CurrentMoney", Mathf.RoundToInt(PlayerPrefs.GetInt("CurrentMoney") * multiplyer));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        //Upgrade cost
//- Reload time
//- Knockback force
//- Fire rate
//- Damage
//- Bullet color
//- Movementspeed
//- Health upgrade
        Debug.Log("Hier!");

        scripts.GetComponent<UpgradeShop>();
        Destroy(GetComponent<LevelManager>());
    }
}
