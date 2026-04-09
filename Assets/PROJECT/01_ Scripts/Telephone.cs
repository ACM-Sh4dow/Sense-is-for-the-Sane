using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    private bool hasBeenTriggered;
    private void StartRinging()
    {
        
    }
    public void Interact()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;
        
        //end ringing
        //start text / sound
        
        Transition();
    }
    
    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }
}
