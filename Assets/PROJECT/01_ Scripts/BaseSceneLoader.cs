using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLoader : MonoBehaviour
{
    public List<string> SceneNames = new ();
    
    private void Start()
    {
        StartCoroutine(LoadScenesAsync());
    }
    
    private IEnumerator LoadScenesAsync()
    {
        foreach (string sceneName in SceneNames)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) break;
 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            if (asyncLoad.isDone) Debug.Log($"{sceneName} is DONE LOADING.");
        }
        Debug.Log("Scene loading COMPLETE.");
        SceneNames.Add(SceneManager.GetActiveScene().name);
    }
}
