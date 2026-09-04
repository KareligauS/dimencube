using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private string sceneToReload = "Karel";
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform titleText;
    [SerializeField] private float fadeDuration = 0.75f;
    [SerializeField] private float scaleDuration = 0.4f;

    private void Start()
    {
        canvasGroup.alpha = 0f;
        titleText.localScale = Vector3.zero;
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        elapsed = 0f;
        while (elapsed < scaleDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Sin(elapsed / scaleDuration * Mathf.PI * 0.5f);
            titleText.localScale = Vector3.one * t;
            yield return null;
        }
        titleText.localScale = Vector3.one;
    }

    public void Retry()
    {
        SceneManager.LoadScene(sceneToReload);
    }
}
