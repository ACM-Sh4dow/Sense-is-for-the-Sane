using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class YellowBed : MonoBehaviour, InteractionPoint
{
    private bool hasBeenTriggered;
    [SerializeField] private bool isCasket;
    public void Interact()
    {
        if (hasBeenTriggered) return;
        hasBeenTriggered = true;
        
        if (isCasket)
        {
            StartCoroutine(Overseer.Instance.GetManager<UiManager>().FadeToWhite());
            return;
        }
        
        AkUnitySoundEngine.SetState("CurrentScene", "Apartment");
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        StartCoroutine(Overseer.Instance.GetManager<UiManager>().FadeToWhite());

        while (UiManager.screenState == UiManager.ScreenState.Fading)
        {
            yield return null;
        }
        PlayerBehaviour.Instance.transform.position = Overseer.Instance.GetManager<VoidManager>().apartmentSpawn.position;
        Overseer.Instance.GetManager<VoidManager>().ActivateShadows();
        StartCoroutine(Overseer.Instance.GetManager<UiManager>().FadeToGame());
    }
}
