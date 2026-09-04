using System.Collections;
using VContainer;
using UnityEngine;

public class lever : MonoBehaviour
{
    public float moveDuration = 2f;
    private bool pulled = false;
    private bool isPlayerClose = false;
    public Transform target;
    public Vector3 targerPosOffset = new Vector3(0, -20, 0);

    [Inject] private readonly AudioManager _audioManager;
    [Inject] private readonly VanishingTextUI _vanishingTextUI;

    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E) && !pulled)
        {
            pulled = true;

            _audioManager.PlaySFX(AudioClipEnum.Switch);
            _vanishingTextUI.ShowMessage("Lever pulled!", 2f);

            Debug.Log("lever pulled!");
            StartCoroutine(MoveLamp());
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerClose = true;

            _vanishingTextUI.ShowMessage("Press E to pull lever.", 2f);

            Debug.Log("press e to pull lever");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }

    private IEnumerator MoveLamp()
    {
        _audioManager.PlaySFX(AudioClipEnum.Chandelier);

        Vector3 startPos = target.localPosition;
        Vector3 endPos = startPos + targerPosOffset;
        float timer = 0;

        while (timer < moveDuration)
        {
            target.localPosition = Vector3.Lerp(startPos,endPos, timer/moveDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        target.localPosition = endPos;
    }
}
