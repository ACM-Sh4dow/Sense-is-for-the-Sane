using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Variables

    [Tooltip("Set to FromStart and save before reloading scene for clear hierarchy. Leave on FromStart to play game start to finish.")]
    private enum LoadScenes
    {
        FromStart,
        AllScenes,
        Void,
        Apartment,
        FuneralHome
    }

    [SerializeField] private LoadScenes loadScenes;

    [SerializeField] private List<string> voidScenes = new();
    [SerializeField] private List<string> apartmentScenes = new();
    [SerializeField] private List<string> funeralHomeScenes = new();
    private List<string> allGameScenes = new();
    private List<Scene> openScenes = new();
    private List<Scene> scenesToRemove = new();

    public enum CurrentLevel
    {
        Void,
        Apartment,
        FuneralHome
    }

    private CurrentLevel currentLevel;

    #endregion

    private void Start()
    {
        Overseer.Instance.AddManager(this);
        if (loadScenes == LoadScenes.FromStart) StartCoroutine(LoadScenesAsync(voidScenes));
    }

    #region Transition
    public void StartTransition(CurrentLevel level)
    {
        currentLevel = level;
        UpdateScenesToRemove();
        
        switch (currentLevel)
        {
            case CurrentLevel.Void:
                StartCoroutine(LoadScenesAsync(apartmentScenes));
                break;
            case CurrentLevel.Apartment:
                StartCoroutine(LoadScenesAsync(funeralHomeScenes));
                break;
            case CurrentLevel.FuneralHome:
                break;
        }
    }

    public void EndTransition(CurrentLevel level)
    {
        currentLevel = level;
        switch (currentLevel)
        {
            case CurrentLevel.Void:
                break;
            case CurrentLevel.Apartment:
                StartCoroutine(UnloadPreviousScenes());
                break;
            case CurrentLevel.FuneralHome:
                StartCoroutine(UnloadPreviousScenes());
                break;
        }
    }
    
    private void UpdateScenesToRemove()
    {
        foreach (Scene scene in openScenes) 
        {
            scenesToRemove.Add(scene);
        }
        foreach (Scene scene in scenesToRemove)
        {
            openScenes.Remove(scene);
        }
    }
    #endregion
    
    #region Loading
    private IEnumerator LoadScenesAsync(List <string> sceneNames)
    {
        foreach (string sceneName in sceneNames)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) break;
 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            openScenes.Add(SceneManager.GetSceneByName(sceneName));
            if (asyncLoad.isDone) Debug.Log($"{sceneName} is DONE LOADING.");
        }
        Debug.Log("Scene loading COMPLETE.");
    }
    private IEnumerator UnloadPreviousScenes()
    {
        foreach (Scene scene in scenesToRemove)
        {
            if (!scene.isLoaded) break;
            scenesToRemove.Remove(scene);
            var sceneName = scene.name;
 
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            if (asyncLoad.isDone) Debug.Log($"{sceneName} has been UNLOADED.");
        }
        Debug.Log("Scene removal COMPLETE.");
    }
    #endregion

    #region Editor
    private void OnValidate()
    {
        switch (loadScenes)
        {
            case LoadScenes.AllScenes:
                allGameScenes = voidScenes.Union(apartmentScenes).Union(funeralHomeScenes).ToList();
                LoadScenesInEditor(allGameScenes);
                break;
            case LoadScenes.Void:
                LoadScenesInEditor(voidScenes);
                break;
            case LoadScenes.Apartment:
                LoadScenesInEditor(apartmentScenes);
                break;
            case LoadScenes.FuneralHome:
                LoadScenesInEditor(funeralHomeScenes);
                break;
        }
    }

    private void LoadScenesInEditor(List<string> sceneNames)
    {
        /* THIS SHOULD WORK BUT IT CRASHES UNITY FOR SOME REASON
             int totalOpenScenes = SceneManager.sceneCount;
            for (int currentScene = 2; currentScene <= totalOpenScenes; currentScene++)
            {
                string sceneName = SceneManager.GetSceneAt(currentScene - 1).name;
                Debug.Log(sceneName);
                EditorSceneManager.CloseScene(SceneManager.GetSceneAt(currentScene - 1), false);
                Debug.Log($"REMOVED scene: {sceneName}");
            }*/
        
        foreach (string sceneName in sceneNames)
        {
            Debug.Log("Scene LOADED: " + sceneName);
            if(SceneManager.GetSceneByName(sceneName).isLoaded) break;
            EditorSceneManager.OpenScene("Assets/PROJECT/00_ Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
        }
    }
    #endregion
}
