using UnityEngine;
using System.Collections;

public class Attack : State
{
    int preDamage;
    int damage;
    float randomRange = 1.5f;


    public override void Enter()
    {
        preDamage = PlayerPrefs.HasKey("EnemyDamage") ? PlayerPrefs.GetInt("EnemyDamage") : 10;
        PlayerPrefs.SetInt("EnemyDamage", preDamage);
        damage = Mathf.RoundToInt(Random.Range(preDamage / randomRange, preDamage * randomRange));
    }

    public override void Execute()
    {
        if (Vector3.Distance(owner.transform.position, Player.instance.transform.position) <= owner.attackDistance && !owner.AttackOnCooldown)
        {
            Player.instance.health -= damage;
            Player.instance.healthText.text = "Health: " + Player.instance.health + "/" + Player.instance.maxHealth;
            owner.AttackOnCooldown = true;
        }
        else if (Vector3.Distance(owner.transform.position, Player.instance.transform.position) > owner.attackDistance)
        {
            owner.stateManager.ChangeState(new Follow());
        }
    }

    public override void Exit()
    {

    }
}
