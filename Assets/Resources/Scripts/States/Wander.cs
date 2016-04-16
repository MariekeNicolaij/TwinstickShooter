using UnityEngine;
using System.Collections;

public class Wander : State
{
    Vector2[] directions = { Vector2.left, Vector2.right };
    Vector2 direction;

    float maxRandomTime = 6;
    float randomIdleTime;
    float randomWalkTime;

    bool walk;

    public override void Start()
    {
        maxRandomTime = 6;
        randomIdleTime = 0;
        randomWalkTime = 0;
        walk = false;

        SetRandomIdleTime();
    }

    public override void Update()
    {
        WalkTimer();
        Move();
    }

    public override void Stop()
    {
        walk = false;
    }

    void SetRandomIdleTime()
    {
        randomIdleTime = Random.Range(0, maxRandomTime);
    }

    void SetRandomWalkTime()
    {
        randomWalkTime = Random.Range(0, maxRandomTime);
    }

    void SetRandomDirection()
    {
        direction = directions[Random.Range(0, directions.Length)];
    }

    void WalkTimer()
    {
        if (walk)
        {
            randomWalkTime -= Time.smoothDeltaTime;

            if (randomWalkTime < 0)
            {
                SetRandomIdleTime();
                walk = false;
            }

            if (owner.GetComponent<Rigidbody>().velocity != Vector3.zero)
                randomWalkTime += 0.001f;
        }
        else
        {
            randomIdleTime -= Time.smoothDeltaTime;

            if (randomIdleTime < 0)
            {
                SetRandomWalkTime();
                SetRandomDirection();
                walk = true;
            }
        }
    }

    void Move()
    {
        owner.transform.Translate(direction * Time.smoothDeltaTime * System.Convert.ToInt32(walk) * owner.speed);
    }
}
