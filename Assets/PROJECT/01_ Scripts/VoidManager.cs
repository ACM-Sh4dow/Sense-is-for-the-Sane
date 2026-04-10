using System.Collections.Generic;
using UnityEngine;

public class VoidManager : MonoBehaviour
{
    private Light light;
    [SerializeField] private Material whiteMaterial;
    
    [SerializeField] private List<GameObject> walls = new();
    [SerializeField] private List<GameObject> blackRoomPieces = new();

    public enum Rooms
    {
        Black,
        White,
        Red,
        Yellow
    }
    public static Rooms inRoom;
    
    void Start()
    {
        Overseer.Instance.AddManager(this);
        light = GameObject.Find("Directional Light").GetComponent<Light>();
        DeactivateShadows();
    }

    public void TransitionToRoom()
    {
        switch (inRoom)
        {
            case Rooms.White:
                WhiteRoom();
                break;
        }
    }
    private void WhiteRoom()
    {
        foreach (GameObject wall in walls)
        {
            wall.GetComponent<MeshRenderer>().material = whiteMaterial;
        }

        foreach (GameObject piece in blackRoomPieces)
        {
            piece.SetActive(false);
        }
    }
    
    private void DeactivateShadows()
    {
        light.shadows = LightShadows.None;
    }
    private void ActivateShadows()
    {
        light.shadows = LightShadows.Soft;
    }
}
