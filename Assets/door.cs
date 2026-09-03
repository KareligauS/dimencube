using UnityEngine;

public class door : MonoBehaviour
{
    private manager m;
    private bool isPlayerClose = false;
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
