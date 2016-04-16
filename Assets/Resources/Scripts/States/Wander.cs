using UnityEngine;
using System.Collections;

public class Wander : State
{
    Vector3 RandomPoint;
    Player player;

    public override void Enter()
    {
        GenerateRandomPos();
        player = Player.instance;
    }

    public override void Execute()
    {
        Debug.DrawLine(owner.transform.position, RandomPoint);

        if (Vector3.Distance(owner.transform.position,player.transform.position) <= owner.followDistance)
        {
            owner.stateManager.ChangeState(new Follow());
        }
        else if (Vector3.Distance(owner.transform.position,RandomPoint) <= owner.attackDistance)
        {
            GenerateRandomPos();
        }
        else
        {
            owner.agent.SetDestination(RandomPoint);
        }
    }

    public override void Exit() { }

    private void GenerateRandomPos()
    {
        Vector3 pos = new Vector3(Random.Range(owner.terrain.GetPosition().x, owner.terrain.terrainData.size.x),
                    owner.terrain.GetPosition().y,
                    Random.Range(owner.terrain.GetPosition().z, owner.terrain.terrainData.size.z));

        NavMeshHit hit = new NavMeshHit();

        NavMesh.SamplePosition(pos, out hit, 100, NavMesh.AllAreas);

        RandomPoint = hit.position;
    }
}
