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
            telephone.clearToStartRinging = true;
            gameObject.SetActive(false);
        }
    }
}
