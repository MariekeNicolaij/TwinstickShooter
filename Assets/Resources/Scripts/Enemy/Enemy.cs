using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    StateManager stateManager;
    ParticleSystem bloodParticle;

    Money money;

    public int health = 100;
    public int maxHealth;
    public float speed = 2.5f;
    float randomRange = 1.5f;
    int moneyForce = 250;

    int preScore, score;

    float moneyFloat;
    int dropMoney;

    float attackDistance = 2;
    float followDistance = 10;

    bool isPlayingParticle;
    bool droppedTheMoney;


    public void Start()
    {
        stateManager = new StateManager();
        bloodParticle = GetComponentInChildren<ParticleSystem>();

        stateManager.owner = this;
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
        preScore = PlayerPrefs.GetInt("Score", 1000);
        score = Mathf.RoundToInt(Random.Range(preScore / randomRange, preScore * randomRange));
    }

    void SetMoneyDrop()
    {
        moneyFloat = PlayerPrefs.GetInt("MoneyDrop", 10);
    }

    void Update()
    {
        stateManager.Update();
        SetState();
        HealthCheck();
    }

    void SetState()
    {
        if (Vector3.Distance(transform.position, Player.instance.transform.position) < attackDistance)
        {
            if (stateManager.currentState.ToString() != "Attack")
                stateManager.ChangeState(new Attack());
        }
        else if (Vector3.Distance(transform.position, Player.instance.transform.position) < followDistance)
        {
            if (stateManager.currentState.ToString() != "Follow")
                stateManager.ChangeState(new Follow());
        }
        else
        {
            if (stateManager.currentState.ToString() != "Wander")
                stateManager.ChangeState(new Wander());
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
