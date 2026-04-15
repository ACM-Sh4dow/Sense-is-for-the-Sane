using System;
using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    private bool transitionHasBeenTriggered;
    public bool clearToStartRinging;
    [SerializeField] private float secondsBeforeRinging = 10f;
    public IEnumerator StartRinging()
    {
        clearToStartRinging = false;

        yield return new WaitForSeconds(secondsBeforeRinging);
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
        if (transitionHasBeenTriggered) return;
        transitionHasBeenTriggered = true;
        AkUnitySoundEngine.PostEvent("Apt_Phone_Stop", gameObject);
        
        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }
}
