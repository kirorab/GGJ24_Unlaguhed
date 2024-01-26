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
    public GameObject fadeImage;
    private Image _image;
    public float fadeSpeed = 1.0f;

    private void Start()
    {
        fadeImage.SetActive(false);
        _image = fadeImage.GetComponent<Image>();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        fadeImage.SetActive(true);
        FadeOut();
    }
    
    private IEnumerator FadeIn()
    {
        fadeImage.SetActive(true);
        float alpha = _image.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
            yield return null;
        }
        fadeImage.SetActive(false);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float alpha = _image.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
            yield return null;
        }
        // 可以在这里添加场景切换的代码
        SceneSystem.Instance.LoadScene(EScene.GameScene);
    }
}
