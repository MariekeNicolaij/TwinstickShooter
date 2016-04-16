using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    int damage;
    int bulletDamage;
    float randomRange = 1.2f;

    float bulletAliveTime = 5;

    Color[] randomColor = { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };


    public void SetBullet(Vector3 startPosition, Vector3 direction)
    {
        transform.position = startPosition;
        transform.localEulerAngles = direction;

        bulletAliveTime = 5;
        damage = PlayerPrefs.GetInt("BulletDamage", 10);
        //GetComponent<Renderer>().material.color = randomColor[Random.Range(0, randomColor.Length)];
        RandomDamage();
    }

    void Update()
    {
        BulletTimer();
    }

    void BulletTimer()
    {
        bulletAliveTime -= Time.smoothDeltaTime;

        if (bulletAliveTime < 0)
            PoolManager.DestroyBullet(this);
    }

    void RandomDamage()
    {
        bulletDamage = Mathf.RoundToInt(Random.Range(damage / randomRange, damage * randomRange));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Enemy)
        {
            RandomDamage();
            other.gameObject.GetComponent<Enemy>().ApplyDamage(bulletDamage);
        }
        if (other.gameObject.layer != (int)Layers.Player &&
            other.gameObject.layer != (int)Layers.Bullet &&
            other.gameObject.layer != (int)Layers.Money)
            PoolManager.DestroyBullet(this);
    }
}