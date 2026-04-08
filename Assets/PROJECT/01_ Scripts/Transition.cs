using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Transition : MonoBehaviour, InteractionPoint
{
    public Vector3 transitionDirection;
    public GameObject retrograde;

    public string audioStateName;
    public AK.Wwise.Event transitionAudioEvent;

    [Tooltip("SET to True if this transition point is in the Alternate Funeral Home, SET False if in Regular.")]
    [SerializeField] private bool isAlternateTransitionPoint; 

    public void Interact()
    {
        Teleport();
    }

    public void Teleport()
    {
        PlayerBehaviour.Instance.transform.position += transitionDirection;
        retrograde.SetActive(!retrograde.activeSelf);

        AkUnitySoundEngine.SetState("FuneralHomeVersion", audioStateName);
        transitionAudioEvent.Post(PlayerBehaviour.Instance.gameObject);
    }
}
