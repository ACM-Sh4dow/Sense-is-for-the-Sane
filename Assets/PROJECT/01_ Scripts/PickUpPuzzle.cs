using UnityEngine;

public class PickUpPuzzle : MonoBehaviour, InteractionPoint
{
    [SerializeField] private float secondsToDisplay = 4.5f;
    public enum Item
    {
        Painting,
        Flower
    }
    public Item item;
    public void Interact()
    {
        switch (item)
        {
            case Item.Painting:
                Overseer.Instance.GetManager<FuneralManager>().paintingItemCollected = true;
                AkUnitySoundEngine.PostEvent("Fnrl_Painting_Pick_Up", PlayerBehaviour.Instance.gameObject);
                break;
            case Item.Flower:
                Overseer.Instance.GetManager<FuneralManager>().flowerItemCollected = true;
                AkUnitySoundEngine.PostEvent("Fnrl_Flower_Vase", PlayerBehaviour.Instance.gameObject);
                break;
        }
        
        Overseer.Instance.GetManager<FuneralManager>().OnPuzzleComplete();
        Overseer.Instance.GetManager<UiManager>().ActivateTextPopup(1, secondsToDisplay);
        gameObject.SetActive(false);
    }
}
