using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    public Camera camera;
    public void ReceiveMovementInput(InputAction.CallbackContext input)
    {
        if (input.started) PlayerBehaviour.Instance.Begin<Walking>();
        if (input.canceled)
        {
            PlayerBehaviour.Instance.End<Walking>();
            return;
        }
        
        Walking.SyncInput(input.ReadValue<Vector2>());
    }

    public void ReceiveLookInput(InputAction.CallbackContext input)
    {
        if (input.started) PlayerBehaviour.Instance.Begin<Looking>();
        if (input.canceled) 
        {
            PlayerBehaviour.Instance.End<Looking>();
            return;
        }
        Looking.SyncInput(input.ReadValue<Vector2>());
    }

    public void ReceiveInteractInput(InputAction.CallbackContext input)
    {
        if (!input.started) return;

        float interactionRange = 5;

        Ray centerRay = camera.ScreenPointToRay(new Vector3(
            Screen.width / 2,
            Screen.height / 2,
            0f));

        if (Physics.Raycast(centerRay, out RaycastHit hitInfo , interactionRange))
        {
            Debug.Log("raycast hit");
            if (hitInfo.collider.TryGetComponent<InteractionPoint>(out InteractionPoint interaction))
            {
                Debug.Log("attempting interact");
                interaction.Interact();
            }
        }

        ReceiveAlign();
    }

    private void ReceiveAlign()
    {
        var puzzle = FindNearestPuzzle();
        if (puzzle == null)
        {
            Debug.LogError("ReceiveAlign: NO nearby puzzle found !!!");
            return;
        }
        Debug.Log($"ReceiveAlign: Nearest PUZZLE is: {puzzle.name} !!!");
        puzzle.AttemptPuzzle();
    }

    private Puzzle FindNearestPuzzle()
    {
        Vector3 playerPos = PlayerBehaviour.Instance.playerPosition;
        var found = FindTarget.List<Puzzle>(playerPos, playerPos, 2);

        if (found.Count <= 0) return null; 

        return FindTarget.Closest(found);
    }

    // public void ReceiveManualAnimationInput(InputAction.CallbackContext input)
    // {
    //     if (input is { started: false, canceled: false }) return;
    //
    //     ManualAnimationProgression.SyncManualAnimationInput();
    // }
}
