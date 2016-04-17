using UnityEngine;
using System.Collections;

public class Follow : State
{
    Vector3 playerPos;
    Player player;

    public override void Enter()
    {
        player = Player.instance;
        playerPos = player.transform.position;
    }

    public override void Execute()
    {
        if (playerPos != player.transform.position)
            playerPos = player.transform.position;

        if (Vector3.Distance(playerPos,owner.transform.position) <= owner.attackDistance)
        {
            owner.stateManager.ChangeState(new Attack());
        }
        else if (Vector3.Distance(playerPos,owner.transform.position)<= owner.followDistance)
        {
            owner.agent.SetDestination(playerPos);
        }
        else
        {
            owner.stateManager.ChangeToDefault();
        }
    }

    public override void Exit()
    {

    }
}
