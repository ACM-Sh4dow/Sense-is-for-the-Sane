using System.Collections.Generic;
using UnityEngine;

public class VoidManager : MonoBehaviour
{
    private Light light;
    [SerializeField] private Material whiteMaterial;
    
    [SerializeField] private List<GameObject> walls = new();
    [SerializeField] private List<GameObject> blackRoomPieces = new();
    [SerializeField] private List<GameObject> whiteRoomPiecesStartActive = new();
    [SerializeField] private List<GameObject> whiteRoomPiecesEndActive = new();
    [SerializeField] private List<GameObject> redRoomPieces = new();
    [SerializeField] private List<GameObject> yellowRoomPieces = new();
    [SerializeField] private Transform playerSpawnYellow;

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
        AkUnitySoundEngine.SetSwitch("FootstepsSwitch", "Void", PlayerBehaviour.Instance.gameObject);
    }

    public void TransitionToRoom()
    {
        switch (inRoom)
        {
            case Rooms.White:
                foreach (GameObject wall in walls)
                {
                    wall.GetComponent<MeshRenderer>().material = whiteMaterial;
                }
                ActivateRoom(blackRoomPieces, whiteRoomPiecesStartActive);
                break;
            case Rooms.Red:
                ActivateRoom(whiteRoomPiecesEndActive, redRoomPieces);
                break;
            case Rooms.Yellow:
                ActivateRoom(redRoomPieces, yellowRoomPieces);
                PlayerBehaviour.Instance.transform.position = playerSpawnYellow.position;
                break;
        }
    }
    private void ActivateRoom(List<GameObject> deactivatePieces, List<GameObject> activatePieces)
    {
        foreach (GameObject piece in deactivatePieces)
        {
            piece.SetActive(false);
        }
        foreach (GameObject piece in activatePieces)
        {
            piece.SetActive(true);
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
