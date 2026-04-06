using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLoader : MonoBehaviour
{
    [SerializeField] private List<string> SceneNames = new ();
    
    private void Start()
    {
        LoadScenesAsync();
    }

    private IEnumerator LoadScenesAsync()
    {
        foreach (string sceneName in SceneNames)
        {
            Debug.Log("1");
            if (SceneManager.GetSceneByName(sceneName).isLoaded || SceneManager.GetSceneByName(sceneName) == null) break;
            Debug.Log("2");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            Debug.Log("3");
            while (!asyncLoad.isDone)
            {
                Debug.Log(asyncLoad.progress);
                yield return null;
            }
            
            if (asyncLoad.isDone) Debug.Log($"{sceneName} is DONE LOADING.");
        }
            yield return null;
    }
}
