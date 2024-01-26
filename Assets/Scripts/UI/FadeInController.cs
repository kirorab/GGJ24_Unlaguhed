using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    private IEnumerator FadeIn()
    {
        float alpha = fadeImage.color.a;
        
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
    }
}
