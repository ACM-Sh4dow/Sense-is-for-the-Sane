using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject quitButton;

    //future note make sure that SceneManagement isn't static and that it has MonoBehaviour
    public static void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //When starting a new game it will delete all save data
    public void StartGame()
    {
        StartCoroutine(DeleteThenStart());
    }
    //part of StartGame
    private IEnumerator DeleteThenStart()
    {
        Saving_And_Loading.DeleteAllData();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(1);
    }

    //Will load the save data
    public void LoadData()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public static void NextLevel(string sceneName)
    {

    }

    //Will close the application
    public static void Quit()
    {
        Application.Quit();
    }

    #region Pausing and resuming

    //Will pause when you press the set button on inputdata (not yet assigned)
    public void Pause(InputAction.CallbackContext inputData)
    {
        PauseFunction();
    }

    //same as above pressing the same button should undo the pause
    public void ResumeButton()
    {
        PauseFunction();
    }

    private void PauseFunction()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            resumeButton.SetActive(false);
            quitButton.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Time.timeScale = 0f;
            resumeButton.SetActive(true);
            quitButton.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion
}
