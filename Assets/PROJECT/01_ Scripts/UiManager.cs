using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public enum ScreenState
    {
        Fading,
        Game,
        ColorScreen
    }
    public static ScreenState screenState;
    
    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Color screenColor;
    [SerializeField] private GameObject toBeContinued;
    public GameObject loadingScreen;
    public GameObject[] uiText;
    private bool textDisplaying;
    private int previousTextIndex;
    private IEnumerator currentTextCoroutine;
    
    void Start()
    {
        Overseer.Instance.AddManager(this);
        screenState = ScreenState.Game;
    }

    public IEnumerator FadeToWhite()
    {
        if (screenState == ScreenState.ColorScreen) yield break;
        screenState = ScreenState.Fading;
        whiteScreen.SetActive(true);

        float fadeRate = 0;
        while (fadeRate < 1)
        {
            fadeRate += Time.deltaTime / fadeSpeed;
            whiteScreen.GetComponent<Image>().color = Color.Lerp(Color.clear, screenColor, fadeRate);
            yield return null;
        }
        screenState = ScreenState.ColorScreen;
    }

    public IEnumerator FadeToGame()
    {
        if (screenState == ScreenState.Game) yield break;
        screenState = ScreenState.Fading;

        float fadeRate = 0;
        while (fadeRate < 1)
        {
            fadeRate += Time.deltaTime / fadeSpeed;
            whiteScreen.GetComponent<Image>().color = Color.Lerp(screenColor, Color.clear, fadeRate);
            yield return null;
        }
        whiteScreen.SetActive(false);
        screenState = ScreenState.Game;
    }

    public IEnumerator EndGame()
    {
        StartCoroutine(FadeToWhite());

        while (screenState == ScreenState.Fading)
        {
            yield return null;
        }
        toBeContinued.SetActive(true);
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(0);
    }

    public void ActivateTextPopup(int uiTextIndex, float secondsToDisplay = 3.5f)
    {
        if (textDisplaying) 
        {
            if (previousTextIndex == uiTextIndex)
            {
                return;
            }
            else
            {
                StopCoroutine(currentTextCoroutine);
                ClearText(previousTextIndex);
            }
        }
        currentTextCoroutine = DisplayText(uiTextIndex, secondsToDisplay);
        StartCoroutine(currentTextCoroutine);
        previousTextIndex = uiTextIndex;
    }

    private IEnumerator DisplayText(int uiTextIndex, float secondsToDisplay)
    {
        textDisplaying = true;
        uiText[uiTextIndex].SetActive(true);
        yield return new WaitForSeconds(secondsToDisplay);
        uiText[uiTextIndex].SetActive(false);
        textDisplaying = false;
    }
    private void ClearText(int uiTextIndex)
    {
        textDisplaying = false;
        uiText[uiTextIndex].SetActive(false);
    }
}
