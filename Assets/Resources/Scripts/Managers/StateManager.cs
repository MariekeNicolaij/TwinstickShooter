using UnityEngine;
using System.Collections;

public class StateManager
{
    public Enemy owner;
    public State currentState;


    public void Start()
    {
        ChangeState(new Wander());
    }

    public void Update()
    {
        if (currentState != null && owner.health > 0)
            currentState.Update();
    }

    public void ChangeState(State state)
    {
        if (state != null)
        {
            state.owner = owner;
            if (currentState != null)
                currentState.Stop();
            currentState = state;
            currentState.Start();
        }
    }
}