using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void RestartLevel(string sceneName)
    {
        
    }

    public static void NextLevel()
    {

    }

}
