using UnityEngine;
using UnityEngine.InputSystem;

interface IInteracable
{
    public void Interact();
}

public class Interaction : MonoBehaviour
{
    public Transform InteractionSource;
    public float InteractionRange;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractionSource.position, InteractionSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractionRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteracable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
