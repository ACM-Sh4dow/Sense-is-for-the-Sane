using System;
using System.Collections.Generic;
using UnityEngine;

public class JackBehaviour : MonoBehaviour
{
    public List<Behaviour> CurrentBehaviours = new List<Behaviour>();
    
    public List<Behaviour> BehavioursToRemove = new List<Behaviour>();
    public List<Behaviour> BehavioursToAdd = new List<Behaviour>();
    
    public static JackBehaviour Instance;
    
    public CapsuleCollider playerCollider;
    public LayerMask collisionLayers;
    
    public GameObject CameraHolder;

    public void RunAll(List<Behaviour> behaviours)
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.Run();
        }
    }
    public void Begin<T>() where T : Behaviour, new()
    {
        if (InState<T>()) return;

        var newBehaviour = new T();
        
        BehavioursToAdd.Add(newBehaviour);
    }
    public void End<T>() where T : Behaviour
    {
        foreach (Behaviour behaviour in CurrentBehaviours)
        {
            if (behaviour is T)
            {
                BehavioursToRemove.Add(behaviour);
            }
        }
    }
    public void ClearOldBehaviours()
    {
        foreach (Behaviour behaviour in BehavioursToRemove)
        {
            behaviour.End();
            CurrentBehaviours.Remove(behaviour);
        }
        BehavioursToRemove.Clear();
    }
    public void AddNewBehaviours()
    {
        foreach (Behaviour behaviour in BehavioursToAdd)
        {
            behaviour.Begin();
            CurrentBehaviours.Add(behaviour);
        }
        BehavioursToAdd.Clear();
    }
    public bool InState<T>() where T : Behaviour
    {
        foreach (Behaviour behaviour in CurrentBehaviours)
        {
            if (behaviour is T) return true;
        }
        return false;
    }

    private void Start()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        RunAll(CurrentBehaviours);
        ClearOldBehaviours();
        AddNewBehaviours();
    }
}
