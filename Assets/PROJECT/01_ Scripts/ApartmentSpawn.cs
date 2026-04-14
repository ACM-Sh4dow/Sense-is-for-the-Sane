using System;
using UnityEngine;

public class ApartmentSpawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<SceneTransition>().TriggerTransition();
            gameObject.SetActive(false);
        }
    }
}
