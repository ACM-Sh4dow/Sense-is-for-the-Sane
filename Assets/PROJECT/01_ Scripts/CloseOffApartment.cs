using System;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CloseOffApartment : MonoBehaviour
{
    [SerializeField] private GameObject funeralHomeDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<SceneTransition>().TriggerTransition();
            funeralHomeDoor.SetActive(true);
            AkUnitySoundEngine.SetState("CurrentScene", "FuneralHome");
            AkUnitySoundEngine.PostEvent("Fnrl_Front_Door_Close", funeralHomeDoor);
            Walking.movementSpeed = 2.5f;
            Walking.SecondsBetweenFootsteps = 0.83f;
            gameObject.SetActive(false);
        }
    }
}
