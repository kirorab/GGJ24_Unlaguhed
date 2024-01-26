using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MainMenuManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.0f;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        FadeOut();
    }
    
    

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float alpha = fadeImage.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
        // 可以在这里添加场景切换的代码
        SceneSystem.Instance.LoadScene(EScene.GameScene);
    }
}
