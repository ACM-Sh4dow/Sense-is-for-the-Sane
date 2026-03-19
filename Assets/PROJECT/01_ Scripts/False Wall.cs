using System;
using Unity.VisualScripting;
using UnityEngine;

public class FalseWall : MonoBehaviour
{
    private Camera camera;
    private Renderer renderer;
    private Collider collider;

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
}
