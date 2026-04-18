using System;
using System.Collections;
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
            
            GetComponent<Collider>().enabled = false;
            StartCoroutine(IncreaseWalkSpeed(2.5f, 4f));
        }
    }

    private IEnumerator IncreaseWalkSpeed(float targetSpeed, float time)
    {
        float increment = ((targetSpeed - Walking.movementSpeed) / time);
        
        while (Walking.movementSpeed < targetSpeed)
        {
            Walking.movementSpeed += increment / 10;
            
            yield return new WaitForSeconds(0.1f);
        }

        Walking.movementSpeed = targetSpeed;
        Walking.SecondsBetweenFootsteps = 0.83f;

        gameObject.SetActive(false);
    }
}
