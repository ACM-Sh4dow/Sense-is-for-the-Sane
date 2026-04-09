using System;
using UnityEngine;

public class CloseOffApartment : MonoBehaviour
{
    private enum colliderType
    {
        transitionTrigger,
        invisibleWall
    }
    [Tooltip("Invisible Wall is blocking the player from returning to the apartment. Transition Trigger is unloading previous scenes.")]
    [SerializeField] private colliderType colliderIs;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited collider.");
            switch (colliderIs)
            {
                case colliderType.invisibleWall:
                    transform.GetComponent<BoxCollider>().isTrigger = false; //possible soft lock if the player walks backwards into AP
                    //set music state
                    break;
                case colliderType.transitionTrigger:
                    transform.GetComponent<SceneTransition>().TriggerTransition();
                    //close door
                    break;
            }
        }
    }
}
