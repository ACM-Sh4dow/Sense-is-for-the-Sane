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
            AkUnitySoundEngine.SetSwitch("FootstepsSwitch", "Wood", PlayerBehaviour.Instance.gameObject);
            Walking.movementSpeed = 1.5f;
            Walking.SecondsBetweenFootsteps = 0.92f;
            telephone.clearToStartRinging = true;
            gameObject.SetActive(false);
        }
    }
}
