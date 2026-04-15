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
            StartCoroutine(telephone.StartRinging());
            gameObject.SetActive(false);
        }
    }
}
