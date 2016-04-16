using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : SpawnManager
{
    float damage = 1;
    float aliveTime = 5;


    void Update()
    {
        AliveTimer();
    }

    void AliveTimer()
    {
        aliveTime -= Time.smoothDeltaTime;

        if (aliveTime < 0)
            DeleteObject(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {



        DeleteObject(gameObject);
    }
}