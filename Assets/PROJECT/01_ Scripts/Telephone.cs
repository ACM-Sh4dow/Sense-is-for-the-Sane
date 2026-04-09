using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, InteractionPoint
{
    [SerializeField] private float secondsToWaitForStoryLines;
    private void StartRinging()
    {
        
    }
    public void Interact()
    {
        //endringing
        //start text / sound
        StartCoroutine(WaitForStoryLines());

    }

    private IEnumerator WaitForStoryLines()
    {
        yield return new WaitForSeconds(secondsToWaitForStoryLines);
        Transition();
    }

    private void Transition()
    {
        transform.GetComponent<SceneTransition>().TriggerTransition();
    }
}
