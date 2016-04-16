using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    static PoolManager instance;

    Pool<Bullet> bulletPool = new Pool<Bullet>();
    Pool<Enemy> enemyPool = new Pool<Enemy>();
    Pool<Money> moneyPool = new Pool<Money>();

    public GameObject bulletPrefab, enemyPrefab, moneyPrefab;


    void Start()
    {
        instance = this;
        bulletPool.prefab = bulletPrefab;
        enemyPool.prefab = enemyPrefab;
        moneyPool.prefab = moneyPrefab;
    }

    public static Bullet SpawnBullet(Vector3 startPosition, Vector3 direction)
    {
        Bullet bullet;

        instance.bulletPool.Spawn(out bullet);

        //if (instance.bulletPool.Spawn(out bullet))
        {
            bullet.SetBullet(startPosition, direction);
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

    public static Money SpawnMoney()
    {
        Money money;

        instance.moneyPool.Spawn(out money);

        return money;
    }

    public static void DestroyBullet(Bullet bullet)
    {
        instance.bulletPool.Destroy(bullet);
    }

    public static void DestroyEnemy(Enemy enemy)
    {
        instance.enemyPool.Destroy(enemy);
    }

    public static void DestroyMoney(Money money)
    {
        instance.moneyPool.Destroy(money);
    }
}
