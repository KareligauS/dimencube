using UnityEngine;
using VContainer;

public class bouncy : MonoBehaviour
{
    [Inject] private readonly AudioManager _audioManager;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            return;
        }

        _audioManager.PlaySFX(AudioClipEnum.Bounce);
    }
}
