using UnityEngine;
using System.Collections;

public class StateManager
{
    public Enemy owner;
    public State currentState;

    private State defaultState;

    public StateManager(Enemy owner, State defaultState)
    {
        this.owner = owner;
        this.defaultState = defaultState;
    }

    public void Start()
    {
        ChangeState(defaultState);
    }

    public void Update()
    {
        if (currentState != null && owner.health > 0)
            currentState.Execute();
    }

    public void ChangeState(State state)
    {
        if (state != null)
        {
            state.owner = owner;
            if (currentState != null)
                currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }

    public void ChangeToDefault()
    {
        ChangeState(defaultState);
    }
}