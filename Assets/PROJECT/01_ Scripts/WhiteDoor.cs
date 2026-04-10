using System;
using UnityEngine;

public class WhiteDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        VoidManager.inRoom = VoidManager.Rooms.White;
        Overseer.Instance.GetManager<VoidManager>().TransitionToRoom();
    }
}
