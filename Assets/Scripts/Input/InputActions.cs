using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public Vector2 movement;
    public bool interact;
    private void Update()
    {
        movement = _inputSystem.Player.Move.ReadValue<Vector2>();
        interact = _inputSystem.Player.Interact.WasPressedThisFrame();
    }
    
    private void Awake() => _inputSystem = new InputSystem_Actions();
    
    private void OnEnable() => _inputSystem.Enable();
    
    private void OnDisable() => _inputSystem.Disable();
}
