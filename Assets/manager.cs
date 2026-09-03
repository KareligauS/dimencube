using Unity.Mathematics;
using UnityEngine;

public class manager : MonoBehaviour
{
    public bool hasKey = false;
    public Transform player;
    public Transform roomPivot;
    private Vector3 savedPos;
    private Quaternion savedRotation;

    void Start()
    {
        savedPos = player.position;
        savedRotation = roomPivot.rotation;
    }

    public void UpdateSpawnPoint(Vector3 newPos, Quaternion newRotation)
    {
        savedPos = newPos;
        savedRotation = newRotation;
        Debug.Log("checkpoint saved");
    }

    public void Respawn()
    {
        roomPivot.rotation = savedRotation;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        player.position = savedPos;
    }

    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("key got!");
    }

    public void TryUnlockDoor()
    {
        if (hasKey)
        {
            Debug.Log("you win!");
        }
        else
        {
            Debug.Log("door is locked!");
        }
    }

}
