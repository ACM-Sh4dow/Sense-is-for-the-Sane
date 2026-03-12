using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteracable
{
    public void Interact();
}

public class Interaction : MonoBehaviour
{
    public float InteractionRange;

    private GameObject ActiveObject;
    private Transform OriginalPosition;
    private ProtagonistController controller;

    // Update is called once per frame
    private void Start()
    {
        // Getting reference from ProtagonistController script
        controller = GetComponent<ProtagonistController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractionRange))
            {
                // Unlocks the cursor
                Cursor.lockState = CursorLockMode.None;

                // Lock player movement
                controller.islocked = true;

                ActiveObject = hitInfo.collider.gameObject;
                OriginalPosition = ActiveObject.transform;

                // Saving the xyz of the roation
                float x = OriginalPosition.transform.rotation.x;
                float y = OriginalPosition.transform.rotation.y;
                float z = OriginalPosition.transform.rotation.z;
                float w = OriginalPosition.transform.rotation.w;


                PlayerPrefs.SetFloat("RotationX", x);
                PlayerPrefs.SetFloat("RotationY", y);
                PlayerPrefs.SetFloat("RotationZ", z);
                PlayerPrefs.SetFloat("RotationW", w);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Ray r = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractionRange))
            {
                // Locks the cursor
                Cursor.lockState = CursorLockMode.Locked;

                // Player can now move
                controller.islocked = false;

                // Loading the xyz of the rotation
                float x = PlayerPrefs.GetFloat("RotationX");
                float y = PlayerPrefs.GetFloat("RotationY");
                float z = PlayerPrefs.GetFloat("RotationZ");
                float w = PlayerPrefs.GetFloat("RotationW");

                quaternion savedRotation = new quaternion(x, y, z, w);

                // Reset object to original rotation
                OriginalPosition.transform.rotation = savedRotation;
            }
        }
    }
}
