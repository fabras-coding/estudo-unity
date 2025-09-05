using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
    void HandleInput();
}
