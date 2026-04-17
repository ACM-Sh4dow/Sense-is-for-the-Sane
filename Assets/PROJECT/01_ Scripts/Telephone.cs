using System;
using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    private bool transitionHasBeenTriggered;
    public bool clearToStartRinging;
    private bool phoneHasRung;
    [SerializeField] private Transform frontDoor;
    [SerializeField] private float secondsBeforeRinging = 10f;
    [SerializeField] private float secondsToDisplayText = 7f;
    private bool frontDoorOpened;

    public IEnumerator StartRinging()
    {
        clearToStartRinging = false;

        yield return new WaitForSeconds(secondsBeforeRinging);
        phoneHasRung = true;
        Debug.Log("Telephone: Phone is ringing!");
        AkUnitySoundEngine.PostEvent("Apt_Phone_Start", gameObject);
    }

    private void Update()
    {
        if (Overseer.Instance.GetManager<SceneLoader>().loadState != SceneLoader.LoadState.None) return;

        if (clearToStartRinging)
        {
            StartCoroutine(StartRinging());
        }

        if (transitionHasBeenTriggered && !frontDoorOpened)
        {
            OpenFrontDoor();
        }
        
    }

    public void Interact()
    {
        if (transitionHasBeenTriggered || !phoneHasRung) return;
        transitionHasBeenTriggered = true;
        AkUnitySoundEngine.PostEvent("Apt_Phone_Stop", gameObject);
        Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(0, secondsToDisplayText);

        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }

    private void OpenFrontDoor()
    {
        frontDoorOpened = true;
        frontDoor.GetComponent<Animator>().enabled = true;
        AkUnitySoundEngine.PostEvent("Apt_Front_Door_Open", frontDoor.gameObject);
    }
}
