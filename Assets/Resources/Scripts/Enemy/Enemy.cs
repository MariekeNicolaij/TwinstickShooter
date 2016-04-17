using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public StateManager stateManager;
    ParticleSystem bloodParticle;

    Money money;

    [HideInInspector]
    public Terrain terrain;

    [HideInInspector]
    public NavMeshAgent agent;

    public int health = 100;
    public int maxHealth;
    public float speed = 2.5f;
    float randomRange = 1.5f;
    int moneyForce = 250;

    int preScore, score;

    float moneyFloat;
    int dropMoney;

    public float attackDistance = 2;
    public float followDistance = 10;

    bool isPlayingParticle;
    bool droppedTheMoney;

    public float AttackCooldown = 5;
    [HideInInspector]
    public bool AttackOnCooldown = false;

    private float counter = 0;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        terrain = EnemyManager.instance.terrain;

        stateManager = new StateManager(this,new Wander());
        bloodParticle = GetComponentInChildren<ParticleSystem>();

        stateManager.Start();
        isPlayingParticle = false;
        droppedTheMoney = false;

        SetHealth();
        SetScore();
        SetMoneyDrop();

    }

    void SetHealth()
    {
        health = PlayerPrefs.GetInt("EnemyHealth", 100);
        health = Mathf.RoundToInt(Random.Range(health / randomRange, health * randomRange));
        maxHealth = health;
    }

    void SetScore()
    {
        preScore = PlayerPrefs.GetInt("DropScore", 1000);
        score = Mathf.RoundToInt(Random.Range(preScore / randomRange, preScore * randomRange));
    }

    void SetMoneyDrop()
    {
        moneyFloat = PlayerPrefs.GetInt("MoneyDrop", 10);
    }

    void Update()
    {
        stateManager.Update();
        HealthCheck();

        if (AttackOnCooldown)
        {
            if (counter >= AttackCooldown)
            {
                AttackOnCooldown = false;
                counter = 0;
            }
            counter += Time.deltaTime;
        }
    }

    public void ApplyDamage(int damage)
    {
        if (health > 0)
            health -= damage;
        if (health < 0)
            health = 0;
    }

    void HealthCheck()
    {
        if (health <= 0)
            Death();
    }

    void Death()
    {
        if (!isPlayingParticle)
        {
            bloodParticle.Play();
            isPlayingParticle = true;
            if (!droppedTheMoney)
            {
                DropMoney();
                Score();
            }
        }
        if (bloodParticle.isStopped)
        {
            Debug.Log("Particle stopped");
            EnemyManager.DespawnEnemy(this);
        }
    }

    void DropMoney()
    {
        dropMoney = Mathf.RoundToInt(Random.Range(moneyFloat / randomRange, moneyFloat * randomRange));
        money = PoolManager.SpawnMoney();
        money.transform.position = transform.position;
        money.GetComponent<Rigidbody>().AddForce(Vector2.up * moneyForce);
        money.value = dropMoney;
        droppedTheMoney = true;
    }

    void Score()
    {
        Player.instance.score += score;
        Player.instance.scoreText.text = "Score: " + score;
        ShowText.instance.AddValue(score, false, "+");
        PlayerPrefs.SetInt("CurrentScore", Player.instance.score);
    }
}
