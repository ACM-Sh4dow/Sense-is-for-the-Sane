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
    public IEnumerator StartRinging()
    {
        clearToStartRinging = false;

        yield return new WaitForSeconds(secondsBeforeRinging);
        phoneHasRung = true;
        AkUnitySoundEngine.PostEvent("Apt_Phone_Start", gameObject);
    }

    private void Update()
    {
        if (!clearToStartRinging) return;

        if (Overseer.Instance.GetManager<SceneLoader>().loadState != SceneLoader.LoadState.None) return;

        StartCoroutine(StartRinging());
    }

    public void Interact()
    {
        if (transitionHasBeenTriggered || !phoneHasRung) return;
        transitionHasBeenTriggered = true;
        AkUnitySoundEngine.PostEvent("Apt_Phone_Stop", gameObject);
        
        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        while (Overseer.Instance.GetManager<SceneLoader>().loadState == SceneLoader.LoadState.Loading)
        {
            yield return null;
        }
        frontDoor.GetComponent<DistanceBasedAnimation>().enabled = true;
    }
}
