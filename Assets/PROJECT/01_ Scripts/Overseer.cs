using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates all other manager objects
/// Creates the singleton instance reference to access managers easily.
/// </summary>
public class Overseer : MonoBehaviour
{
    public static Overseer Instance { get; private set; }

    private Dictionary<Type, MonoBehaviour> managerRegistry = new();

    private Type[] managerTypes = new Type[]
    {
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeManagers();
    }

    private void InitializeManagers()
    {
        foreach (var type in managerTypes)
        {
            InitializeManager(type);
        }
    }

    private void InitializeManager(Type type)
    {
        if (managerRegistry.ContainsKey(type)) return;

        GameObject obj = new GameObject(type.Name);
        obj.transform.SetParent(transform);
        MonoBehaviour manager = obj.AddComponent(type) as MonoBehaviour;

        if (manager == null)
        {
            Debug.LogError($"Failed to add {type.Name} as MonoBehaviour.");
            return;
        }

        managerRegistry[type] = manager;
    }

    public T GetManager<T>() where T : MonoBehaviour
    {
        Type type = typeof(T);
        if (managerRegistry.TryGetValue(type, out MonoBehaviour manager))
        {
            return manager as T;
        }

        Debug.LogError($"Manager of type {type.Name} not found.");
        return null;
    }

    public void AddManager(MonoBehaviour newManager)
    {
        if (managerRegistry.ContainsKey(newManager.GetType())) return;

        if (newManager == null)
        {
            Debug.LogError($"Failed to add {newManager} as MonoBehaviour.");
            return;
        }

        managerRegistry[newManager.GetType()] = newManager;
    }
}