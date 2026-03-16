using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Transition : MonoBehaviour, InteractionPoint
{
    public Vector3 transitionDirection;
    public GameObject retrograde;

    public void Interact()
    {
        PlayerBehaviour.Instance.transform.position += transitionDirection;
        retrograde.SetActive(!retrograde.activeSelf);
    }
}
