using UnityEngine;

public class manager : MonoBehaviour
{
    public bool hasKey = false;
    public Transform player;
    public Transform spawnPoint;

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

    public void PlayerDie()
    {
        Debug.Log("you died!");
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        playerRb.linearVelocity = Vector3.zero;
        player.position = spawnPoint.position;
    }
}
