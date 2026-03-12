using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public void ReceiveMovementInput(InputAction.CallbackContext input)
    {
        if (input.started) JackBehaviour.Instance.Begin<Hopping>();
        if (input.canceled)
        {
            JackBehaviour.Instance.End<Hopping>();
            return;
        }
        
        Hopping.SyncInput(input.ReadValue<Vector2>());
    }

    public void RecieveLookInput(InputAction.CallbackContext input)
    {
        if (input.started) JackBehaviour.Instance.Begin<Looking>();
        if (input.canceled) 
        {
            JackBehaviour.Instance.End<Looking>();
            return;
        }
        Looking.SyncInput(input.ReadValue<Vector2>());
    }

    public void RecieveJumpInput(InputAction.CallbackContext input)
    {
        if (!input.started) return;
        if (JackBehaviour.Instance.InState<Falling>() && Falling.StartTime + Falling.CoyoteTime > Time.time) return;
        if (JackBehaviour.Instance.InState<Jumping>()) return;
        if (GroundCheck.Execute(JackBehaviour.Instance.playerCollider, JackBehaviour.Instance.collisionLayers) == null) return;
        
        JackBehaviour.Instance.Begin<Jumping>();
    }
}
