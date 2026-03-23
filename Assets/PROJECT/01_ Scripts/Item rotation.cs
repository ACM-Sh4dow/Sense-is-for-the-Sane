using UnityEngine;
using UnityEngine.InputSystem;

public class Item_rotation : MonoBehaviour
{
    public Transform itemToExamine;

    public float rotationSpeed = 100f;

    private Vector3 MousePosition;

    void Update()
    {
        if (itemToExamine == null) return;

        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            MousePosition = mouse.position.ReadValue();
        }

        if (mouse.leftButton.isPressed)
        {
            Vector3 currentPos = mouse.position.ReadValue();
            Vector3 deltaMousePosition = currentPos - MousePosition;

            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            itemToExamine.rotation = rotation * itemToExamine.rotation;

            MousePosition = currentPos;
        }
    }
}