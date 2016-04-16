using UnityEngine;
using System.Collections;

public class Attack : State
{
    float damage;
    float realDamage;

    public override void Start()
    {
        GetSetDamage();
    }

    void GetSetDamage()
    {
        damage = PlayerPrefs.GetInt("EnemyDamage", 5);
    }

    public override void Update()
    {

    }

    public override void Stop()
    {

    }
}
