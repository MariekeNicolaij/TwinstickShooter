using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static Player instance;
    public SpawnManager spawnManager;

    public Text healthText;
    public Text scoreText;
    public Text moneyText;

    public float health;
    public float moveSpeed;
    public float fireRate = 500;
    public float knockbackForce = 75;
    public int score;
    public int money;

    [Range(0, 1)]
    public float shootDelay = 0.2f;
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
        health = PlayerPrefs.GetInt("PlayerHealth", 100);
        moveSpeed = PlayerPrefs.GetInt("PlayerMovementSpeed", 3);
        fireRate = PlayerPrefs.GetInt("FireRate", 500);
        knockbackForce = PlayerPrefs.GetInt("KnockbackForce", 100);
        score = PlayerPrefs.GetInt("CurrentScore", 0);
        money = PlayerPrefs.GetInt("CurrentMoney", 0);
    }

    public void Update()
    {
        ShootTimer();
    }

    void OnTriggerEnter(Collider other)
    {
        CollectMoney(other);
    }

    bool CanMovePointedDirection(float x, float y)
    {

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

        Vector3 dir = new Vector3(0, transform.localEulerAngles.y, 0);

        Bullet bullet = PoolManager.SpawnBullet(startPos, dir);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * fireRate);
    }

    void ShootTimer()
    {
        if (canShoot)
            return;

        if (shootDelay < 0)
        {
            canShoot = true;
            shootDelay = 0.2f;
        }

        shootDelay -= Time.smoothDeltaTime;
    }

    void AddKnockback(Vector3 direction)
    {
        //GetComponent<Rigidbody>().AddForce(direction * knockbackForce);
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
}