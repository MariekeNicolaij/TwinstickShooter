using UnityEngine;
using System.Collections;

public class Attack : State
{
    Player player;

    public override void Enter()
    {
        player = Player.instance;
    }

    public override void Execute()
    {
        if (Vector3.Distance(owner.transform.position, player.transform.position) <= owner.attackDistance && !owner.AttackOnCooldown)
        {
            //Player.GetDamaged(owner.damage);
            Debug.Log("Take that bitch");
            owner.AttackOnCooldown = true;
        }
        else if (Vector3.Distance(owner.transform.position, player.transform.position) > owner.attackDistance)
        {
            owner.stateManager.ChangeState(new Follow());
        }
    }

    public override void Exit()
    {

    }
}
