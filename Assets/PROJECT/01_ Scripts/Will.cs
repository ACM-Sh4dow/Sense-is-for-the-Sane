using UnityEngine;
using UnityEngine.Analytics;

public class Will : MonoBehaviour, InteractionPoint
{
    private enum Location
    {
        regular,
        alternate
    }
    [SerializeField] private Location location;
    public void Interact()
    {
        switch (location)
        {
            case Location.regular:
                Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(5, 5);
                break;
            case Location.alternate:
                Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(6, 5);
                break;
        }
    }
}
