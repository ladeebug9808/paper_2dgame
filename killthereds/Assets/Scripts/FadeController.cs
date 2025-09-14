using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeDuration;

    public bool fadeIn;

    public bool fadeOut;

    void Start()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        if (fadeOut)
        {
            FadeOut();
        }
  }

  public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTiime = 0.0f;
        while (elapsedTiime < fadeDuration)
        {
            elapsedTiime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTiime / duration);
            yield return null;
        }
        cg.alpha = end;
    }

}
