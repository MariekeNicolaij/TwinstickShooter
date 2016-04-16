using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeShop : MonoBehaviour
{
    public GameObject ingame;
    public GameObject upgradeShop;

    RectTransform upgradeShopRectTransform;
    Vector2 endPos;
    Vector2 maxSize;

    bool lerp;
    float lerpPosTime, lerpSizeTime;
    float lerpSpeed = 0.001f;
    float maxOffset = 1;


    public void Start()
    {
        Time.timeScale = 0;
        upgradeShopRectTransform = upgradeShop.GetComponent<RectTransform>();

        endPos = upgradeShopRectTransform.position;
        upgradeShopRectTransform.position = new Vector2(Screen.width / 2, Screen.height);
        maxSize = upgradeShopRectTransform.sizeDelta;
        upgradeShopRectTransform.sizeDelta = new Vector2(Screen.height / 10, Screen.width / 10);

        lerp = true;
    }

    void Update()
    {
        if (lerp)
            LerpScreen();
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

    public void BuyUpgrade(Upgrade upgrade)
    {

    }

    public void NextLevel()
    {
        PlayerPrefs.SetString("LoadLevel", Application.loadedLevelName);
        Application.LoadLevel("Loading");
    }
}

public enum Upgrade
{
    Health,
    MovementSpeed,
    KnockbackForce,
    Damage,
    ReloadTime,
    FireRate,
    BulletColor
}