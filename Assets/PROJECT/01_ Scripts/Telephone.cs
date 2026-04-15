using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    private bool hasBeenTriggered;
    [SerializeField] private float secondsBeforeRinging = 10f;
    public IEnumerator StartRinging()
    {
        yield return new WaitForSeconds(secondsBeforeRinging);
        //start sound
    }
    public void Interact()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;
        
        //end ringing
        
        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }
}
