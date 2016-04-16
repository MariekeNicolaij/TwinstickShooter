using UnityEngine;
using System.Collections;

public class Follow : State
{
    Vector2 playerPos;

    public override void Start()
    {
        
    }

    public override void Update()
    {
        FollowPlayer();
    }

    public override void Stop()
    {

    }

    void FollowPlayer()
    {
        playerPos = Player.instance.transform.position;
        //owner.transform.Translate(playerPos * Time.smoothDeltaTime * owner.speed);
        Vector3.MoveTowards(owner.transform.position, playerPos, owner.speed * Time.smoothDeltaTime);
    }
}
