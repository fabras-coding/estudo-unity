// using System;
// using System.Collections.Generic;
// using UnityEngine;

// public class StateMachine
// {
//     public enum State { Idle, Walk, Run, Jump, Attack }

//     private class StateData
//     {
//         public Action Enter;
//         public Action Update;
//         public Action Exit;
//     }

//     private Dictionary<State, StateData> _states = new();
//     private State _currentState;
//     private StateData _currentData;

//     public void AddState(State state, Action onEnter, Action onUpdate, Action onExit)
//     {
//         _states[state] = new StateData
//         {
//             Enter = onEnter,
//             Update = onUpdate,
//             Exit = onExit
//         };
//     }

//     public void ChangeState(State newState)
//     {
//         if (_currentState.Equals(newState)) return;

//         _currentData?.Exit();
//         _currentState = newState;
//         _currentData = _states[_currentState];
//         _currentData?.Enter();
//     }

//     public void Update()
//     {
//         _currentData?.Update();
//     }

//     public State CurrentState => _currentState;
// }
