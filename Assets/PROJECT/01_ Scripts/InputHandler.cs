using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    public void ReceiveMovementInput(InputAction.CallbackContext input)
    {
        ProtagonistController.SyncMovementInput(input);
    }

    public void ReceiveLookInput(InputAction.CallbackContext input)
    {
        ProtagonistController.SyncLookInput(input.ReadValue<Vector2>());
    }

    public void ReceiveInteractInput(InputAction.CallbackContext input)
    {
        if (!input.started) return;

        ProtagonistController.Interact();
        if (ProtagonistController.Instance.perspectivePuzzle != null)
        {
            ProtagonistController.Instance.perspectivePuzzle.SolvePuzzle();
        }
    }

    public void ReceiveManualAnimationInput(InputAction.CallbackContext input)
    {
        if (!input.started && !input.canceled) return;

        ManualAnimationProgression.SyncManualAnimationInput();
    }
}
