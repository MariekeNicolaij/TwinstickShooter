using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public Pickups pickup;
    public int value;

    float onGroundTime;
    float speed = 2.5f;


    public void Start()
    {
        onGroundTime = 5;
    }

    void Update()
    {
        OnGroundTimer();
    }

    void OnGroundTimer()
    {
        onGroundTime -= Time.smoothDeltaTime;

        if (onGroundTime < 0)
        {
            switch (pickup)
            {
                case Pickups.Health:
                    if (Player.instance.health < Player.instance.maxHealth)
                        transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position, Time.smoothDeltaTime * speed);
                    else
                        PoolManager.DestroyHealth(this);
                    break;
                case Pickups.Money:
                    transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position, Time.smoothDeltaTime * speed);
                    break;
                case Pickups.Score:
                    transform.position = Vector3.Lerp(transform.position, Player.instance.transform.position, Time.smoothDeltaTime * speed);
                    break;

            }

        }
    }
}
