using System;
using Unity.VisualScripting;
using UnityEngine;

public class FalseWall : MonoBehaviour, InteractionPoint
{
    private Camera camera;
    private Renderer renderer;
    private Collider collider;

    private enum DoorSide
    {
        Left,
        Right
    }
    [SerializeField] private DoorSide doorSide;

    private void Start()
    {
        camera = PlayerBehaviour.Instance.Camera;
        renderer = GetComponent<Renderer>();
        collider = gameObject.GetComponent<Collider>();
    }

    private bool IsInView()
    {
        Vector3 pointOnScreen = camera.WorldToScreenPoint(renderer.bounds.center);
        
        if (pointOnScreen.z < 0)
        {
            return false;
        }
        
        return true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        transform.parent.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!IsInView())
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }

    public void Interact()
    {
        switch (doorSide)
        {
            case DoorSide.Left:
                Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(4, 5);
                break;
            case DoorSide.Right:
                Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(3, 5);
                break;
        }
    }
    
}
