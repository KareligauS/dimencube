using System.Collections;
using TMPro;
using UnityEngine;

public interface IVanishingTextUI
{
    void ShowMessage(string message, float duration = 10f);
    void ShowDefaultMessage();
}

public class VanishingTextUI : MonoBehaviour, IVanishingTextUI
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private string _defaultMessage = "Move: A / D    Jump: Space    Interact: E";
    [SerializeField] private float _defaultDuration = 10f;
    [SerializeField] private float _fadeDuration = 0.5f;

    private Coroutine _activeRoutine;

    private void Awake()
    {
        _canvasGroup.alpha = 0f;
    }

    private void Start()
    {
        ShowDefaultMessage();
    }

    public void ShowDefaultMessage()
    {
        ShowMessage(_defaultMessage, _defaultDuration);
    }

    public void ShowMessage(string message, float duration = 10f)
    {
        _text.text = message;

        if (_activeRoutine != null)
            StopCoroutine(_activeRoutine);

        _activeRoutine = StartCoroutine(ShowAndVanish(duration));
    }

    private IEnumerator ShowAndVanish(float duration)
    {
        yield return Fade(0f, 1f);
        yield return new WaitForSeconds(duration);
        yield return Fade(1f, 0f);

        _activeRoutine = null;
    }

    private IEnumerator Fade(float from, float to)
    {
        _canvasGroup.alpha = from;
        var elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / _fadeDuration);
            yield return null;
        }

        _canvasGroup.alpha = to;
    }
}
