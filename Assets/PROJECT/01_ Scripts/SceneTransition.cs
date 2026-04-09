using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float secondsToWait;
    private enum TransitionType
    {
        start,
        end
    }
    [SerializeField] private TransitionType transition;
    [SerializeField] private SceneLoader.CurrentLevel currentLevel;

    public void TriggerTransition()
    {
        switch (transition)
        {
            case TransitionType.start:
                Overseer.Instance.GetManager<SceneLoader>().StartTransition(currentLevel, secondsToWait);
                break;
            case TransitionType.end:
                Overseer.Instance.GetManager<SceneLoader>().EndTransition(currentLevel, secondsToWait);
                break;
        }
    }
}
