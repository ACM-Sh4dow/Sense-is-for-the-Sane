using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject creditsPage;
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject menuObjects;
    [SerializeField] GameObject creditsObjects;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image blackScreen;

    [SerializeField] float fadeTime = 3;
    public float timeDifference = 0;

    private bool isPlaying;

    private void Start()
    {
        blackScreen.enabled = true;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(FadeBlackScreen(0, fadeTime, Time.time, true));
    }

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
    public void Play()
    {
        if (isPlaying) return;
        isPlaying = true;
        StartCoroutine(WaitToPlay());
    }
    private IEnumerator WaitToPlay()
    {
        AkUnitySoundEngine.PostEvent("Menu_Stop_All", loadingScreen);
        StartCoroutine(FadeBlackScreen(1, 2, Time.time, false));
        yield return new WaitForSeconds(3);  //  Waiting for fade to finish

        loadingScreen.SetActive(true);
        blackScreen.gameObject.SetActive(false);
        menuButtons.SetActive(false);
        menuObjects.SetActive(false);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        creditsPage.SetActive(true);
        creditsObjects.SetActive(true);
        menuButtons.SetActive(false);
        menuObjects.SetActive(false);
    }
    public void Back()
    {
        creditsPage.SetActive(false);
        creditsObjects.SetActive(false);
        menuButtons.SetActive(true);
        menuObjects.SetActive(true);
    }

    private IEnumerator FadeBlackScreen(float targetOpacity, float duration, float startTime, bool deactivateWhenFinished)
    {
        blackScreen.gameObject.SetActive(true);
        float startOpacity = blackScreen.color.a;
        float timeDiff = 0;

        while (timeDiff < duration)
        {
            timeDiff = (Time.time - startTime);
            timeDifference = timeDiff;

            float alpha = Mathf.Lerp(startOpacity, targetOpacity, timeDiff / duration);

            blackScreen.color = new Color(0, 0, 0, alpha);


            yield return null;
        }

        blackScreen.color = blackScreen.color = new Color(0, 0, 0, targetOpacity);

        if(deactivateWhenFinished) blackScreen.gameObject.SetActive(false);
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
