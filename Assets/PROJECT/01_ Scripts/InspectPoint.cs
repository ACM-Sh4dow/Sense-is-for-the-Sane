using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InspectPoint : MonoBehaviour, InteractionPoint
{
    public bool beingInspected = false;
    private Vector2 currentMousePosition;
    private Vector2 priorMousePosition;

    public void Interact()
    {
        beingInspected = !beingInspected;
        PlayerBehaviour.Instance.BlockBehaviours();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Update()
    {
        priorMousePosition = currentMousePosition;
        currentMousePosition = Mouse.current.position.ReadValue();
        
        if (!beingInspected)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            var mouseDirection = currentMousePosition - priorMousePosition;
            
            this.gameObject.transform.Rotate(mouseDirection);
        }
    }
}