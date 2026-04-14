using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
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
    [SerializeField] private Color secondScreenColor;
    
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
}
