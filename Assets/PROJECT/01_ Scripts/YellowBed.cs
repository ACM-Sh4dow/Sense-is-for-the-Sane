using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class YellowBed : MonoBehaviour, InteractionPoint
{
    private bool hasBeenTriggered;
    public void Interact()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        StartCoroutine(Overseer.Instance.GetManager<ScreenFade>().FadeToWhite());

        while (ScreenFade.screenState == ScreenFade.ScreenState.Fading)
        {
            yield return null;
        }
        PlayerBehaviour.Instance.transform.position = Overseer.Instance.GetManager<VoidManager>().apartmentSpawn.position;
        StartCoroutine(Overseer.Instance.GetManager<ScreenFade>().FadeToGame());
    }
}
