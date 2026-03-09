using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Transition : MonoBehaviour, InteractionPoint
{
    public Vector3 transitionDirection;
    public FullScreenPassRendererFeature retrograde;

    public void Interact()
    {
        ProtagonistController.Instance.transform.position += transitionDirection;
        retrograde.SetActive(!retrograde.isActive);
    }
}
