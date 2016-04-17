using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    static PoolManager instance;

    Pool<Bullet> bulletPool = new Pool<Bullet>();
    Pool<Enemy> enemyPool = new Pool<Enemy>();
    Pool<Pickup> moneyPool = new Pool<Pickup>();
    Pool<Pickup> healthPool = new Pool<Pickup>();

    public GameObject bulletPrefab, enemyPrefab, moneyPrefab, healthPrefab;


    void Start()
    {
        instance = this;
        bulletPool.prefab = bulletPrefab;
        enemyPool.prefab = enemyPrefab;
        moneyPool.prefab = moneyPrefab;
        healthPool.prefab = healthPrefab;
    }

    public static Bullet SpawnBullet(Vector3 startPosition, Vector3 direction, int damage)
    {
        Bullet bullet;

        instance.bulletPool.Spawn(out bullet);

        //if (instance.bulletPool.Spawn(out bullet))
        {
            bullet.SetBullet(startPosition, direction, damage);
        }
        return bullet;
    }

    public static Enemy SpawnEnemy()
    {
        Enemy enemy;

        //if (instance.enemyPool.Spawn(out enemy))        
        instance.enemyPool.Spawn(out enemy);
        enemy.Start();

        return enemy;
    }

    public static Pickup SpawnMoney()
    {
        Pickup pickup;

        instance.moneyPool.Spawn(out pickup);
        pickup.Start();

        return pickup;
    }

    public static Pickup SpawnHealth()
    {
        Pickup pickup;

        instance.healthPool.Spawn(out pickup);
        pickup.Start();

        return pickup;
    }

    public static void DestroyBullet(Bullet bullet)
    {
        instance.bulletPool.Destroy(bullet);
    }

    public static void DestroyEnemy(Enemy enemy)
    {
        instance.enemyPool.Destroy(enemy);
    }

    public static void DestroyMoney(Pickup pickup)
    {
        instance.moneyPool.Destroy(pickup);
    }

    public static void DestroyHealth(Pickup pickup)
    {
        instance.healthPool.Destroy(pickup);
    }
}
