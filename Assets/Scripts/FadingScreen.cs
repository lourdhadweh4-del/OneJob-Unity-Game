using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadingScreen : MonoBehaviour
{
    public Image panelImage;
    public float duration = 2f;
    public float strength = 100f;


    private void Awake()
    {
        panelImage = GetComponent<Image>();
    }

    public void FadeTo(GameObject screen)
    {
        StartCoroutine(FadeIn(duration, screen));
    }

    private IEnumerator FadeIn(float duration, GameObject screen)
    {
        float time = 0;
        Color color = panelImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = time * strength / duration;
            panelImage.color = color;
            yield return null;
        }

        screen.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float time = 0.1f;
        Color color = panelImage.color;

        while (time < 1)
        {
            time += Time.deltaTime * strength;
            color.a = 1f - (time * strength);
            panelImage.color = color;
            yield return null;
        }
    }
}