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

    public enum LoadState
    {
        Loading,
        Unloading,
        None
    }
    public LoadState loadState = LoadState.None;


    [SerializeField] private List<string> voidScenes = new();
    [SerializeField] private List<string> apartmentScenes = new();
    [SerializeField] private List<string> funeralHomeScenes = new();
    private List<string> allGameScenes = new();
    private List<Scene> openScenes = new();
    private List<Scene> scenesToRemove = new();
    
    [SerializeField] private List<Transform> playerSpawns = new();

    private bool loadingScreenOff;

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
        
        switch (loadScenes)
        {
            case LoadScenes.FromStart:
                PlayerBehaviour.Instance.transform.position = playerSpawns[0].position;
                StartCoroutine(LoadScenesAsync(voidScenes));
                break;
            case LoadScenes.Void:
                PlayerBehaviour.Instance.transform.position = playerSpawns[0].position;
                AkUnitySoundEngine.SetState("CurrentScene", "Void");
                break;
            case LoadScenes.Apartment:
                PlayerBehaviour.Instance.transform.position = playerSpawns[1].position;
                AkUnitySoundEngine.SetState("CurrentScene", "Apartment");
                break;
            case LoadScenes.FuneralHome:
                PlayerBehaviour.Instance.transform.position = playerSpawns[2].position;
                AkUnitySoundEngine.SetState("CurrentScene", "FuneralHome");
                break;
        }
    }
    private void Update()
    {
        if (loadingScreenOff) return;
        if(loadState == LoadState.None)
        {
            loadingScreenOff = true;
            Overseer.Instance.GetManager<UiManager>().loadingScreen.SetActive(false);
        }
    }

    #region Transition
    public void StartTransition(CurrentLevel level, float secondsToWait = 0)
    {
        currentLevel = level;
        UpdateScenesToRemove();
        
        switch (currentLevel)
        {
            case CurrentLevel.Void:
                StartCoroutine(LoadScenesAsync(apartmentScenes, secondsToWait));
                break;
            case CurrentLevel.Apartment:
                StartCoroutine(LoadScenesAsync(funeralHomeScenes, secondsToWait));
                break;
            case CurrentLevel.FuneralHome:
                break;
        }
    }

    public void EndTransition(CurrentLevel level, float secondsToWait = 0)
    {
        currentLevel = level;
        switch (currentLevel)
        {
            case CurrentLevel.Void:
                StartCoroutine(UnloadPreviousScenes(secondsToWait, "Void_Stop_All"));
                break;
            case CurrentLevel.Apartment:
                StartCoroutine(UnloadPreviousScenes(secondsToWait));
                break;
            case CurrentLevel.FuneralHome:
                StartCoroutine(UnloadPreviousScenes(secondsToWait, "Apt_Stop_All"));
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
    private IEnumerator LoadScenesAsync(List <string> sceneNames, float secondsToWait = 0)
    {
        loadState = LoadState.Loading;
        Debug.Log($"SceneLoader: Loading new scenes in {secondsToWait} seconds...");
        yield return new WaitForSeconds(secondsToWait);
        
        foreach (string sceneName in sceneNames)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                Debug.Log($"SceneLoader: {sceneName} is ALREADY LOADED.");
                openScenes.Add(SceneManager.GetSceneByName(sceneName));
                continue;
            }
 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            openScenes.Add(SceneManager.GetSceneByName(sceneName));
            if (asyncLoad.isDone) Debug.Log($"SceneLoader: {sceneName} is DONE LOADING.");
        }
        Debug.Log("SceneLoader: Scene loading COMPLETE.");
        loadState = LoadState.None;
    }
    private IEnumerator UnloadPreviousScenes(float secondsToWait = 0, string sceneAudioStopEvent = "")
    {
        loadState = LoadState.Unloading;
        Debug.Log($"SceneLoader: Removing previous scenes in {secondsToWait} seconds...");
        yield return new WaitForSeconds(secondsToWait);

        Debug.Log("Audio: Stopping scene audio - " + sceneAudioStopEvent);
        if(sceneAudioStopEvent != "") AkUnitySoundEngine.PostEvent(sceneAudioStopEvent, gameObject);

        foreach (Scene scene in scenesToRemove)
        {
            if (!scene.isLoaded) break;
            var sceneName = scene.name;
 
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            if (asyncLoad.isDone) Debug.Log($"SceneLoader: {sceneName} has been UNLOADED.");
        }
        scenesToRemove.Clear();
        Debug.Log("SceneLoader: Scene removal COMPLETE.");
        loadState = LoadState.None;
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
         //THIS SHOULD WORK BUT IT CRASHES UNITY FOR SOME REASON
         //    int totalOpenScenes = SceneManager.sceneCount;
         //   for (int currentScene = 2; currentScene <= totalOpenScenes; currentScene++)
         //   {
         //       string sceneName = SceneManager.GetSceneAt(currentScene - 1).name;
         //       Debug.Log(sceneName);
         //       EditorSceneManager.CloseScene(SceneManager.GetSceneAt(currentScene - 1), false);
         //       Debug.Log($"REMOVED scene: {sceneName}");
         //   }
        
        foreach (string sceneName in sceneNames)
        {
            Debug.Log("Scene LOADED: " + sceneName);
            if(SceneManager.GetSceneByName(sceneName).isLoaded) continue;
            EditorSceneManager.OpenScene("Assets/PROJECT/00_ Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
        }
    }
    #endregion
    
}
