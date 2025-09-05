using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public void Initialize(IState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.HandleInput();
            currentState.Update();
        }
    }

    public T GetCurrentState<T>() where T : class, IState
    {
        return currentState as T;
    }

    public bool IsInState<T>() where T : class, IState
    {
        return currentState is T;
    }
}
