using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    private bool hasBeenTriggered;
    [SerializeField] private float secondsBeforeRinging = 10f;
    public IEnumerator StartRinging()
    {
        Debug.Log("TELEPHONE WAITING "  +secondsBeforeRinging);

        yield return new WaitForSeconds(secondsBeforeRinging);
        Debug.Log("TELEPHONE RINGING");
        AkUnitySoundEngine.PostEvent("Apt_Phone_Start", gameObject);
    }
    public void Interact()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;
        AkUnitySoundEngine.PostEvent("Apt_Phone_Stop", gameObject);
        
        
        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }
}
