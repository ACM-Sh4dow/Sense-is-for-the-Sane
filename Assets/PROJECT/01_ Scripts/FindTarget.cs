using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public static class FindTarget
{

    public static List<(T, Vector3)> List<T>(Vector3 detectionStart, Vector3 detectionEnd, float radius)
    {
        List<(T, Vector3)> discovered = new List<(T, Vector3)>();

        Collider[] detectedColliders = Physics.OverlapCapsule(detectionStart, detectionEnd, radius);
        
        foreach (Collider detected in detectedColliders)
        {
            if (detected.TryGetComponent<T>(out T t))
            {
                discovered.Add((t, detected.transform.position));
            }
        }
        return discovered;
    }

    [CanBeNull]
    public static T Closest<T>(List<(T, Vector3)> inputList)
    {
        (T, Vector3) closest = inputList[0];

        foreach ((T, Vector3) item in inputList)
        {
            if (item.Item2.magnitude < closest.Item2.magnitude)
            {
                closest = item;
            }
        }

        return closest.Item1;
    }
}
