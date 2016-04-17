using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy> enemies = new List<Enemy>();
    public List<Vector3> spawnPoints = new List<Vector3>();

    public Text spawnTimeText;
    public int spawnLimit = 5;
    public int maxRandomSpawnCount = 5;
    public float spawnTime = 60;
    float spawnTimeOutTime = 5;

    public Terrain terrain;


    void Awake()
    {
        instance = this;

        if (terrain == null)
            Debug.LogError("setThe Terrain in the enemyManager pls");
    }

    void Update()
    {
        SpawnTimer();
        SpawnTimeOutTimer();
        SpawnEnemies();
    }

    void SpawnTimer()
    {
        if (spawnTime > 0)
        {
            spawnTime -= Time.smoothDeltaTime;
            spawnTimeText.text = "Time: " + Mathf.Round(spawnTime).ToString();
        }
        else
            spawnTimeText.text = "Enemies left: " + enemies.Count;
    }

    void SpawnTimeOutTimer()
    {
        if (spawnTimeOutTime > 0)
            spawnTimeOutTime -= Time.smoothDeltaTime;
    }

    void SpawnEnemies()
    {
        if (spawnTime > 0 && spawnTimeOutTime <= 0 && enemies.Count < spawnLimit)
        {
            float randomSpawnCount = Random.Range(0, maxRandomSpawnCount);
            for (int i = 0; i < randomSpawnCount; i++)
            {
                spawnTimeOutTime = 5;
                enemies.Add(SpawnEnemy());
            }
        }
    }

    Enemy SpawnEnemy()
    {
        Enemy enemy = PoolManager.SpawnEnemy();
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)];

        return enemy;
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

    public static void DespawnEnemy(Enemy enemy)
    {
        instance.enemies.Remove(enemy);
        PoolManager.DestroyEnemy(enemy);
    }
}