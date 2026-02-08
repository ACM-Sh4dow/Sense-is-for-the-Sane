using UnityEngine;
using UnityEngine.InputSystem;

public class Item_rotation : MonoBehaviour
{
    // The object that needs to be inspected
    public Transform itemToExamine;

    // Roation speed of the object when dragging your mouse as it is being inspected
    public float rotationSpeed = 100f;

    // Tracks the last known position of the mouse (Input System uses Vector2)
    private Vector3 MousePosition;

    // Update is called once per frame
    void Update()
    {
        if (itemToExamine == null)
            return;

        // Prefer the new Input System's mouse; fall back is omitted to keep behavior consistent
        var mouse = Mouse.current;
        if (mouse == null)
            return;

        // When left mouse button is pressed (not held) this frame
        if (mouse.leftButton.wasPressedThisFrame)
        {
            // The last mouse position will be the current mouse position
            MousePosition = mouse.position.ReadValue();
        }

        // To check if the left mouse button is being held
        if (mouse.leftButton.isPressed)
        {
            // The deltaMouse position is the difference between the current mouse position and the previous mouse position
            Vector3 currentPos = mouse.position.ReadValue();
            Vector3 deltaMousePosition = currentPos - MousePosition;

            // Based on the delta it will apply it afterwards to the rotation of the object
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            // Quaternion handles the rotation of the object and Quaternion.Euler makes the actual rotation based on the rotation X and Y
            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            // Applied rotation to the object by multiplying the new rotation with the current rotation of the object
            itemToExamine.rotation = rotation * itemToExamine.rotation;

            // Since you will be holding down the left mouse button you will need to update the previous mouse position to the current mouse position
            MousePosition = currentPos;
        }
    }
}