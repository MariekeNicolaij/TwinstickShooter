using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public StateManager stateManager;
    ParticleSystem bloodParticle;

    Pickup money, healthPickup;

    [HideInInspector]
    public Terrain terrain;

    [HideInInspector]
    public NavMeshAgent agent;

    public int health;
    public int maxHealth;
    public float speed = 2.5f;
    float randomRange = 1.5f;
    int dropForce = 250;

    int preScore, score;

    int preMoney, preHealth;
    int dropMoney, dropHealth;

    public float attackDistance = 2;
    public float followDistance = 10;

    bool isPlayingParticle;
    bool dropped;

    public float AttackCooldown = 2.5f;
    [HideInInspector]
    public bool AttackOnCooldown = false;

    private float counter = 0;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        terrain = EnemyManager.instance.terrain;

        stateManager = new StateManager(this, new Wander());
        bloodParticle = GetComponentInChildren<ParticleSystem>();

        stateManager.Start();
        isPlayingParticle = false;
        dropped = false;

        SetHealth();
        SetScore();
        SetMoneyDrop();
        SetHealthDrop();
    }

    void SetHealth()
    {
        health = PlayerPrefs.HasKey("EnemyHealth") ? PlayerPrefs.GetInt("EnemyHealth") : 25;
        health = Mathf.RoundToInt(Random.Range(health / randomRange, health * randomRange));
        PlayerPrefs.SetInt("EnemyHealth", health);
        maxHealth = health;
    }

    void SetScore()
    {
        preScore = PlayerPrefs.HasKey("ScoreDrop") ? PlayerPrefs.GetInt("ScoreDrop") : 1000;
        score = Mathf.RoundToInt(Random.Range(preScore / randomRange, preScore * randomRange));
        PlayerPrefs.SetInt("ScoreDrop", preScore);
    }

    void SetMoneyDrop()
    {
        preMoney = PlayerPrefs.HasKey("MoneyDrop") ? PlayerPrefs.GetInt("MoneyDrop") : 10;
        PlayerPrefs.SetInt("MoneyDrop", preMoney);
    }

    void SetHealthDrop()
    {
        preHealth = PlayerPrefs.HasKey("HealthDrop") ? PlayerPrefs.GetInt("HealthDrop") : 5;
        PlayerPrefs.SetInt("HealthDrop", preHealth);
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
            if (!dropped)
            {
                DropMoney();
                DropHealthPickup();
                Score();
                dropped = true;
            }
        }
        if (bloodParticle.isStopped)
            EnemyManager.DespawnEnemy(this);
    }

    void DropMoney()
    {
        dropMoney = Mathf.RoundToInt(Random.Range(preMoney / randomRange, preMoney * randomRange));
        money = PoolManager.SpawnMoney();
        money.transform.position = transform.position;
        money.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * dropForce);
        money.value = dropMoney;
    }

    void DropHealthPickup()
    {
        dropHealth = Mathf.RoundToInt(Random.Range(preHealth / randomRange, preHealth * randomRange));
        healthPickup = PoolManager.SpawnHealth();
        healthPickup.transform.position = transform.position;
        healthPickup.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * dropForce);
        healthPickup.value = dropHealth;
    }

    void Score()
    {
        Player.instance.score += score;
        Player.instance.scoreText.text = "Score: " + Player.instance.score;
        PickupText.instance.AddValue(Pickups.Score, score, "+");
    }
}
