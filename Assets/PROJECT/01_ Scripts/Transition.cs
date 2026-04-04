using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Transition : MonoBehaviour, InteractionPoint
{
    public Vector3 transitionDirection;
    public GameObject retrograde;

    public string audioStateName;
    public AK.Wwise.Event transitionAudioEvent;

    public void Interact()
    {
        PlayerBehaviour.Instance.transform.position += transitionDirection;
        retrograde.SetActive(!retrograde.activeSelf);

        AkUnitySoundEngine.SetState("FuneralHomeVersion", audioStateName);
        transitionAudioEvent.Post(PlayerBehaviour.Instance.gameObject);
    }
}
