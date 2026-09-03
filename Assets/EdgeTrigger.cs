using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
    private RoomManager roomManager;

    void Start()
    {
        roomManager = FindFirstObjectByType<RoomManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomManager.TriggerEdge(gameObject.tag);
        }
    }
}