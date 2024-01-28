using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    public Image title;
    public Sprite laughSprite;
    public Image[] images;
    public Color laughColor;

    private void Awake()
    {
        if (GameManager.Instance.laughed)
        {
            title.sprite = laughSprite;
            foreach (var image in images)
            {
                image.color = laughColor;
            }
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
