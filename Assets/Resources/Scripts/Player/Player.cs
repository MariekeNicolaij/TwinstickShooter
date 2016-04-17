using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static Player instance;

    public Text healthText;
    public Text scoreText;
    public Text moneyText;

    public int health;
    public int moveSpeed;
    public int bulletSpeed;
    public int bulletDamage;
    public float shootDelay;
    public int knockbackForce;
    public int score;
    public int money;

    float shootDelayTime;

    bool canShoot = true;

    float rotateOffset = 15;
    float bulletOffset = 0.5f;


    void Awake()
    {
        instance = this;

        GetStats();
    }

    void GetStats()
    {
        health = PlayerPrefs.HasKey("PlayerHealth") ? PlayerPrefs.GetInt("PlayerHealth") : 100;
        moveSpeed = PlayerPrefs.HasKey("PlayerMovementSpeed") ? PlayerPrefs.GetInt("PlayerMovementSpeed") : 3;
        bulletSpeed = PlayerPrefs.HasKey("BulletSpeed") ? PlayerPrefs.GetInt("BulletSpeed") : 500;
        bulletDamage = PlayerPrefs.HasKey("BulletDamage") ? PlayerPrefs.GetInt("BulletDamage") : 10;
        shootDelay = PlayerPrefs.HasKey("ShootDelay") ? PlayerPrefs.GetFloat("ShootDelay") : 0.8f;
        knockbackForce = PlayerPrefs.HasKey("KnockbackForce") ? PlayerPrefs.GetInt("KnockbackForce") : 50;
        score = PlayerPrefs.HasKey("CurrentScore") ? PlayerPrefs.GetInt("CurrentScore") : 0;
        money = PlayerPrefs.HasKey("CurrentMoney") ? PlayerPrefs.GetInt("CurrentMoney") : 0;

        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.SetInt("PlayerMovementSpeed", moveSpeed);
        PlayerPrefs.SetInt("BulletSpeed", bulletSpeed);
        PlayerPrefs.SetInt("BulletDamage", bulletDamage);
        PlayerPrefs.SetFloat("ShootDelay", shootDelay);
        PlayerPrefs.SetInt("KnockbackForce", knockbackForce);
        PlayerPrefs.SetInt("CurrentScore", score);
        PlayerPrefs.SetInt("CurrentMoney", money);
    }

    public void Update()
    {
        ShootTimer();
        if (Input.GetKeyDown(KeyCode.M))
            money += 1000;
    }

    void OnTriggerEnter(Collider other)
    {
        CollectMoney(other);
    }

    bool CanMovePointedDirection(float x, float y)
    {
        Debug.Log("Dit moet nog");
        return true;
    }

    public void Move(float x, float y)
    {
        if (!CanMovePointedDirection(x, y))
            return;

        Vector3 direction = new Vector3(x, 0, y);
        transform.position += direction * Time.smoothDeltaTime * moveSpeed;
    }

    public void Rotate(float x, float y)
    {
        Vector3 mousePos = new Vector3(x, y, rotateOffset);
        Vector3 lookPos = new Vector3();

        if (x > 1 || y > 1)
            lookPos = new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x, transform.position.y, Camera.main.ScreenToWorldPoint(mousePos).z);
        else
            lookPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);

        transform.LookAt(lookPos);
    }

    public void Shoot(float leftAxis, float rightAxis)
    {
        if (!canShoot)
            return;

        bool left = leftAxis > 0 ? true : false;
        bool right = rightAxis > 0 ? true : false;

        if (left)
            ShootBullet(bulletOffset);
        if (right)
            ShootBullet(-bulletOffset);

        AddKnockback(-transform.forward);

        canShoot = false;
    }

    void ShootBullet(float offSet)
    {
        Vector3 startPos = transform.position;
        startPos += transform.right * offSet;

        Vector3 direction = new Vector3(0, transform.localEulerAngles.y, 0);

        Bullet bullet = PoolManager.SpawnBullet(startPos, direction, bulletDamage);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    void ShootTimer()
    {
        if (canShoot)
            return;

        if (shootDelayTime < 0)
        {
            canShoot = true;
            shootDelayTime = shootDelay;
        }

        shootDelayTime -= Time.smoothDeltaTime;
    }

    void AddKnockback(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * knockbackForce);
    }

    void CollectMoney(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Money)
        {
            money += other.GetComponent<Money>().value;
            moneyText.text = "Money: $" + money;
            ShowText.instance.AddValue(other.GetComponent<Money>().value, true, "+ $");
            PoolManager.DestroyMoney(other.GetComponent<Money>());
            PlayerPrefs.SetInt("CurrentMoney", money);
        }
    }





    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleLeft;
        style.fontSize = 20;
        style.normal.textColor = Color.black;

        GUI.Box(new Rect(Screen.width * 0.05f, Screen.height * 0.4f, 200, 100),
            "Health: " + health + "\n" +
            "Move Speed: " + moveSpeed + "\n" +
            "Bullet Speed: " + bulletSpeed + "\n" +
            "Bullet Damage: " + bulletDamage + "\n" +
            "Shoot Delay: " + shootDelay + "\n" +
            "Knockback Force: " + knockbackForce + "\n" +
            "Rainbow bullet: " + PlayerPrefs.GetInt("RainbowBullet") + "\n" +
            "Score: " + score + "\n" +
            "Money: " + money, style);
    }
}