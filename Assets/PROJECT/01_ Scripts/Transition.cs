using UnityEngine;

public class Transition : MonoBehaviour, InteractionPoint
{
    public Vector3 transitionDirection;
    
    public void Interact()
    {
        ProtagonistController.Instance.transform.position += transitionDirection;
    }
}
