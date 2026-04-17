using UnityEngine;

public class PickUpPuzzle : MonoBehaviour, InteractionPoint
{
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
                break;
            case Item.Flower:
                Overseer.Instance.GetManager<FuneralManager>().flowerItemCollected = true;
                break;
        }
        
        Overseer.Instance.GetManager<FuneralManager>().OnPuzzleComplete();
        gameObject.SetActive(false);
    }
}
