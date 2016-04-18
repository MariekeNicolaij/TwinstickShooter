using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeShop : MonoBehaviour
{
    public static UpgradeShop instance;

    public GameObject ingame;
    public GameObject upgradeShop;

    public Text statsText;
    public Text moneyText;

    RectTransform upgradeShopRectTransform;
    Vector2 endPos;
    Vector2 maxSize;

    bool lerp;
    float lerpPosTime, lerpSizeTime;
    float lerpSpeed = 0.001f;
    float maxOffset = 1;

    #region Costs
    int healthCost;
    int moveSpeedCost;
    int bulletSpeedCost;
    int bulletDamageCost;
    int shootDelayCost;
    int knockbackForceCost;
    int rainbowBulletsCost;
    float multiplyer = 1.5f;
    int maxMovespeed = 15;
    int maxKnockbackForce = 0;
    float maxShootDelay = 0.1f;
    #endregion

    #region Cost texts
    public Text healthCostText;
    public Text moveSpeedCostText;
    public Text bulletSpeedCostText;
    public Text bulletDamageCostText;
    public Text shootDelayCostText;
    public Text knockbackForceCostText;
    public Text rainbowBulletCostText;
    #endregion

    #region Buttons
    public Button healthButton;
    public Button moveSpeedButton;
    public Button bulletSpeedButton;
    public Button bulletDamageButton;
    public Button shootDelayButton;
    public Button knockbackForceButton;
    public Button rainbowBulletsButton;
    #endregion


    void Awake()
    {
        instance = this;
    }

    public void StartUpgradeShop()
    {
        Time.timeScale = 0;
        upgradeShop.SetActive(true);
        upgradeShopRectTransform = upgradeShop.GetComponent<RectTransform>();

        endPos = upgradeShopRectTransform.position;
        upgradeShopRectTransform.position = new Vector2(Screen.width / 2, Screen.height);
        maxSize = upgradeShopRectTransform.sizeDelta;
        upgradeShopRectTransform.sizeDelta = new Vector2(Screen.height / 10, Screen.width / 10);

        SetCosts();
        SetButtonsGray();
        SetStats();

        lerp = true;
    }

    void SetCosts()
    {
        healthCost = PlayerPrefs.HasKey("HealthCost") ? PlayerPrefs.GetInt("HealthCost") : 50;
        moveSpeedCost = PlayerPrefs.HasKey("MoveSpeedCost") ? PlayerPrefs.GetInt("MoveSpeedCost") : 50;
        bulletSpeedCost = PlayerPrefs.HasKey("BulletSpeedCost") ? PlayerPrefs.GetInt("BulletSpeedCost") : 50;
        bulletDamageCost = PlayerPrefs.HasKey("BulletDamageCost") ? PlayerPrefs.GetInt("BulletDamageCost") : 50;
        shootDelayCost = PlayerPrefs.HasKey("ShootDelayCost") ? PlayerPrefs.GetInt("ShootDelayCost") : 50;
        knockbackForceCost = PlayerPrefs.HasKey("KnockbackForceCost") ? PlayerPrefs.GetInt("KnockbackForceCost") : 50;
        rainbowBulletsCost = 10000;

        PlayerPrefs.SetInt("HealthCost", healthCost);
        PlayerPrefs.SetInt("MoveSpeedCost", moveSpeedCost);
        PlayerPrefs.SetInt("BulletSpeedCost", bulletSpeedCost);
        PlayerPrefs.SetInt("BulletDamageCost", bulletDamageCost);
        PlayerPrefs.SetInt("ShootDelayCost", shootDelayCost);
        PlayerPrefs.SetInt("KnockbackForceCost", knockbackForceCost);

        healthCostText.text = "$" + healthCost;
        moveSpeedCostText.text = "$" + moveSpeedCost;
        bulletSpeedCostText.text = "$" + bulletSpeedCost;
        bulletDamageCostText.text = "$" + bulletDamageCost;
        shootDelayCostText.text = "$" + shootDelayCost;
        knockbackForceCostText.text = "$" + knockbackForceCost;
        rainbowBulletCostText.text = "$" + rainbowBulletsCost;
    }

    void SetButtonsGray()
    {
        healthButton.interactable = Player.instance.money > healthCost;
        moveSpeedButton.interactable = Player.instance.money > moveSpeedCost && Player.instance.moveSpeed < maxMovespeed;
        bulletSpeedButton.interactable = Player.instance.money > bulletSpeedCost;
        bulletDamageButton.interactable = Player.instance.money > bulletDamageCost;
        shootDelayButton.interactable = Player.instance.money > shootDelayCost && Player.instance.shootDelay > maxShootDelay;
        knockbackForceButton.interactable = Player.instance.money > knockbackForceCost && Player.instance.knockbackForce > maxKnockbackForce;
        rainbowBulletsButton.interactable = Player.instance.money > rainbowBulletsCost && !System.Convert.ToBoolean(PlayerPrefs.GetInt("BulletRainbow"));
    }

    void SetStats()
    {
        statsText.text = "Health: " + PlayerPrefs.GetInt("PlayerHealth") + "\n" +
            "Movement Speed: " + PlayerPrefs.GetInt("PlayerMovementSpeed") + "\n" +
            "Bullet Speed: " + PlayerPrefs.GetInt("BulletSpeed") + "\n" +
            "Bullet Damage: " + PlayerPrefs.GetInt("BulletDamage") + "\n" +
            "Shoot Delay: " + PlayerPrefs.GetFloat("ShootDelay") + "\n" +
            "Knockback Force: " + PlayerPrefs.GetInt("KnockbackForce") + "\n" +
            "Rainbow Bullets: " + (System.Convert.ToBoolean(PlayerPrefs.GetInt("BulletRainbow")) ? "Yes" : "No");
        moneyText.text = "$ " + Player.instance.money;
    }

    void Update()
    {
        if (lerp)
            LerpScreen();
        if (upgradeShop.activeInHierarchy)
            SetStats();
    }

    void LerpIngame()
    {
        ingame.transform.position += Vector3.up;
    }

    void LerpScreen()
    {
        LerpIngame();
        if (Vector2.Distance(upgradeShopRectTransform.position, endPos) > maxOffset)
        {
            lerpPosTime += lerpSpeed;
            upgradeShopRectTransform.position = Vector2.Lerp(upgradeShopRectTransform.position, endPos, lerpPosTime);
        }
        else if (Vector2.Distance(upgradeShopRectTransform.sizeDelta, maxSize) > maxOffset)
        {
            lerpSizeTime += lerpSpeed;
            upgradeShopRectTransform.sizeDelta = Vector2.Lerp(upgradeShopRectTransform.sizeDelta, maxSize, lerpSizeTime);
        }
        else
        {
            ingame.SetActive(false);
            lerp = false;
        }
    }

    public void BuyUpgrade(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case (int)Upgrade.Health:
                if (Player.instance.money >= healthCost)
                {
                    Player.instance.money -= healthCost;
                    Player.instance.maxHealth += 50;
                    PlayerPrefs.SetInt("PlayerHealth", Player.instance.maxHealth);
                    PlayerPrefs.SetInt("HealthCost", Mathf.RoundToInt(healthCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.Movespeed:
                if (Player.instance.money >= moveSpeedCost)
                {
                    Player.instance.money -= moveSpeedCost;
                    Player.instance.moveSpeed += 1;
                    PlayerPrefs.SetInt("PlayerMovementSpeed", Player.instance.moveSpeed);
                    PlayerPrefs.SetInt("MoveSpeedCost", Mathf.RoundToInt(moveSpeedCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.BulletSpeed:
                if (Player.instance.money >= bulletSpeedCost)
                {
                    Player.instance.money -= bulletSpeedCost;
                    Player.instance.bulletSpeed += 50;
                    PlayerPrefs.SetInt("BulletSpeed", Player.instance.bulletSpeed);
                    PlayerPrefs.SetInt("BulletSpeedCost", Mathf.RoundToInt(bulletSpeedCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.BulletDamage:
                if (Player.instance.money >= bulletDamageCost)
                {
                    Player.instance.money -= bulletDamageCost;
                    Player.instance.bulletDamage += 1;
                    PlayerPrefs.SetInt("BulletDamage", Player.instance.bulletDamage);
                    PlayerPrefs.SetInt("BulletDamageCost", Mathf.RoundToInt(bulletDamageCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.Shootdelay:
                if (Player.instance.money >= shootDelayCost)
                {
                    Player.instance.money -= shootDelayCost;
                    Player.instance.shootDelay = Mathf.Round(Player.instance.shootDelay * 100 - 5) / 100;
                    PlayerPrefs.SetFloat("ShootDelay", Player.instance.shootDelay);
                    PlayerPrefs.SetInt("ShootDelayCost", Mathf.RoundToInt(shootDelayCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.KnockbackForce:
                if (Player.instance.money >= knockbackForceCost)
                {
                    Player.instance.money -= knockbackForceCost;
                    Player.instance.knockbackForce -= 10;
                    PlayerPrefs.SetInt("KnockbackForce", Player.instance.knockbackForce);
                    PlayerPrefs.SetInt("KnockbackForceCost", Mathf.RoundToInt(knockbackForceCost * multiplyer));
                    SetCosts();
                    SetButtonsGray();
                }
                break;
            case (int)Upgrade.BulletRainbowColor:
                if (Player.instance.money >= rainbowBulletsCost)
                {
                    Player.instance.money -= rainbowBulletsCost;
                    PlayerPrefs.SetInt("BulletRainbow", 1);
                    SetCosts();
                    SetButtonsGray();
                }
                break;
        }
    }

    public void LevelButton()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("LoadLevel", Application.loadedLevelName);
        Application.LoadLevel("Loading");
    }

    public void MenuButton()
    {
        Debug.Log("Menu");
    }
}

public enum Upgrade
{
    Health = 0,
    Movespeed = 1,
    BulletSpeed = 2,
    BulletDamage = 3,
    Shootdelay = 4,
    KnockbackForce = 5,
    BulletRainbowColor = 6
}