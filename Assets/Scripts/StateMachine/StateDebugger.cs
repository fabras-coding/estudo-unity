using UnityEngine;

[System.Serializable]
public class StateDebugger : MonoBehaviour
{
    [Header("State Machine Debug")]
    [SerializeField] private bool enableDebug = true;
    [SerializeField] private string currentStateName = "None";
    [SerializeField] private float stateTimer = 0f;
    
    private ControlTankWarrior tankController;
    private IState lastState;
    
    private void Awake()
    {
        tankController = GetComponent<ControlTankWarrior>();
    }
    
    private void Update()
    {
        if (!enableDebug || tankController?.stateMachine == null) return;
        
        // Update debug info
        var currentState = tankController.stateMachine.GetCurrentState<IState>();
        
        if (currentState != lastState)
        {
            lastState = currentState;
            stateTimer = 0f;
            currentStateName = currentState?.GetType().Name ?? "None";
            
            if (enableDebug)
            {
                Debug.Log($"[StateDebugger] Transitioned to: {currentStateName}");
            }
        }
        
        stateTimer += Time.deltaTime;
    }
    
    private void OnGUI()
    {
        if (!enableDebug) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, 100));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label($"Current State: {currentStateName}");
        GUILayout.Label($"State Timer: {stateTimer:F2}s");
        GUILayout.Label($"Is Grounded: {tankController?.playerController?.isGrounded}");
        GUILayout.Label($"Move Direction: {tankController?.moveDirection}");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
