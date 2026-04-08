using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region Variables
    
    [Tooltip("Is this SceneTransition loading new scenes or unloading previous ones?")]
    public enum State
    {
        loading,
        unloading
    }

    public State state;

    [SerializeField] private string newBaseSceneName;
    
    #endregion

    private IEnumerator LoadNewBaseScene()
    {
        if (SceneManager.GetSceneByName(newBaseSceneName).isLoaded) yield break;
 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newBaseSceneName, LoadSceneMode.Additive);
            
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
            
        if (asyncLoad.isDone) Debug.Log($"{newBaseSceneName} is DONE LOADING.");
    }

    private void UnloadPreviousScenes()
    {
        
    }
}
