using System;
using UnityEngine;

public class VoidDoor : MonoBehaviour
{
    private enum Rooms
    {
        Black,
        White,
        Red,
        Yellow
    }
    [SerializeField] private Rooms inRoom;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        switch (inRoom)
        {
            case Rooms.Black:
                VoidManager.inRoom = VoidManager.Rooms.White;
                break;
            case Rooms.White:
                VoidManager.inRoom = VoidManager.Rooms.Red;
                break;
            case Rooms.Red:
                VoidManager.inRoom = VoidManager.Rooms.Yellow;
                break;
            case Rooms.Yellow:
                //apt
                break;
        }
        Overseer.Instance.GetManager<VoidManager>().TransitionToRoom();
    }
}
