using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class manager : MonoBehaviour
{
    public bool hasKey = false;
    public bool openedTheDoor = false;
    public Transform player;
    public Transform roomPivot;
    [SerializeField] private float doorOpenDelay = 1.5f;
    [SerializeField] private string endSceneName = "End";
    private bool isOpeningDoor = false;
    private Vector3 savedPos;
    private Quaternion savedRotation;

    [Inject] private readonly AudioManager _audioManager;
    [Inject] private readonly VanishingTextUI _vanishingTextUI;

    void Start()
    {
        savedPos = player.position;
        savedRotation = roomPivot.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateSpawnPoint(Vector3 newPos, Quaternion newRotation)
    {
        savedPos = newPos;
        savedRotation = newRotation;

        _vanishingTextUI.ShowMessage("Checkpoint saved!", 2f);

        Debug.Log("checkpoint saved");
    }

    public void Respawn()
    {
        _audioManager.PlaySFX(AudioClipEnum.Splash);
        _audioManager.PlaySFX(AudioClipEnum.Death);

        roomPivot.rotation = savedRotation;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        player.position = savedPos;
    }

    public void CollectKey()
    {
        hasKey = true;

        _audioManager.PlaySFX(AudioClipEnum.Key);
        _vanishingTextUI.ShowMessage("You got the key!", 2f);

        Debug.Log("key got!");
    }

    public void TryUnlockDoor()
    {
        if (!hasKey)
        {
            _vanishingTextUI.ShowMessage("Door is locked. You need the Key!", 5f);

            Debug.Log("door is locked!");
            return;
        }

        if (openedTheDoor)
        {
            SceneManager.LoadScene(endSceneName);
            return;
        }

        if (isOpeningDoor) return;

        StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        isOpeningDoor = true;

        _audioManager.PlaySFX(AudioClipEnum.Door);
        _vanishingTextUI.ShowMessage("Unlocking...", doorOpenDelay);

        yield return new WaitForSeconds(doorOpenDelay);

        openedTheDoor = true;
        isOpeningDoor = false;

        _vanishingTextUI.ShowMessage("Door opened! Press E to leave.", 3f);

        Debug.Log("you win!");
    }
}
