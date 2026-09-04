using UnityEngine;
using VContainer;

public class door : MonoBehaviour
{
    private manager m;
    private bool isPlayerClose = false;

    [Inject] private readonly VanishingTextUI _vanishingTextUI;

    void Start()
    {
        m = FindFirstObjectByType<manager>();
    }

    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            m.TryUnlockDoor();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            isPlayerClose = true;

            _vanishingTextUI.ShowMessage("Press E to interact with door.", 2f);

            Debug.Log("press E to interact with door");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
