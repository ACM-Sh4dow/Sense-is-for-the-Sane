using System;
using UnityEngine;

public class ApartmentSpawn : MonoBehaviour
{
    [SerializeField] private Telephone telephone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<SceneTransition>().TriggerTransition();
            telephone.clearToStartRinging = true;
            gameObject.SetActive(false);
        }
    }
}
