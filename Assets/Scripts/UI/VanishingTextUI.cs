using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Color _backgroundColor = new Color(0f, 0f, 0f, 0.6f);
    [SerializeField] private Vector2 _backgroundPadding = new Vector2(16f, 8f);

    private RectTransform _backgroundRect;

    private Coroutine _activeRoutine;

    private void Awake()
    {
        CreateBackground();
        _canvasGroup.alpha = 0f;
    }

    private void CreateBackground()
    {
        var textRect = _text.rectTransform;

        var backgroundGO = new GameObject("TextBackground", typeof(RectTransform), typeof(Image));
        _backgroundRect = (RectTransform)backgroundGO.transform;
        _backgroundRect.SetParent(textRect.parent, false);
        _backgroundRect.anchorMin = textRect.anchorMin;
        _backgroundRect.anchorMax = textRect.anchorMax;
        _backgroundRect.pivot = textRect.pivot;
        _backgroundRect.anchoredPosition = textRect.anchoredPosition;
        _backgroundRect.SetSiblingIndex(textRect.GetSiblingIndex());

        var image = backgroundGO.GetComponent<Image>();
        image.color = _backgroundColor;
        image.raycastTarget = false;
    }

    private void UpdateBackgroundSize()
    {
        _text.ForceMeshUpdate();
        var textSize = _text.textBounds.size;
        _backgroundRect.sizeDelta = (Vector2)textSize + _backgroundPadding * 2f;
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
        UpdateBackgroundSize();

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
