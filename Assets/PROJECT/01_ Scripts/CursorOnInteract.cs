using UnityEngine;

public class CursorOnInteract : MonoBehaviour
{
    public Camera camera;
    void Update()
    {
        Vector3 playerPos = PlayerBehaviour.Instance.playerPosition;
        var found = FindTarget.List<InteractionPoint>(playerPos, playerPos, 2);

        if (found != null)
        {
            float interactionRange = 5;

            Ray centerRay = camera.ScreenPointToRay(new Vector3(
                Screen.width / 2,
                Screen.height / 2,
                0f));

            if (Physics.Raycast(centerRay, out RaycastHit hitInfo , interactionRange))
            {
                if (hitInfo.collider.TryGetComponent<InteractionPoint>(out InteractionPoint interaction))
                {
                    Debug.Log("found interaction point in range : " + found);
                    PlayerBehaviour.Instance.activateCursor = true;
                }
                else
                {
                    PlayerBehaviour.Instance.activateCursor = false;
                }
            }
        }
    }
}
