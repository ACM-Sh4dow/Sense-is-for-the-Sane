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

    private GameObject ActiveObject;
    private Transform OriginalPosition;

    private ProtagonistController controller;

    // Update is called once per frame

    private void Start()
    {
        controller = GetComponent<ProtagonistController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractionRange))
            {
                Cursor.lockState = CursorLockMode.None;


                controller.islocked = true;
                //lock player movement

                ActiveObject = hitInfo.collider.gameObject;
                OriginalPosition = ActiveObject.transform;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Ray r = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractionRange))
            {
                Cursor.lockState = CursorLockMode.Locked;
                // ActiveObject.transform.rotation = OriginalPosition.rotation.eulerAngles(;

                controller.islocked = false;
                //reset object to original rotation
                // ActiveObject.transform set it to OriginalPosition
            }
        }
    }
}
