using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform roomPivot;
    public Transform player;
    public PlayerController playerController;
    public float rotationDuration = 0.5f;

    private bool isRotating = false;
    private bool isCooldown = false;

    void Update()
    {
        // testing
        // if (Input.GetKeyDown(KeyCode.Q) && !isRotating) StartCoroutine(RotateRoom(1));
        // if (Input.GetKeyDown(KeyCode.E) && !isRotating) StartCoroutine(RotateRoom(-1));
    }

    public void TriggerEdge(string edgeTag)
    {
        if (isRotating || isCooldown) return;
        if (edgeTag == "RightEdge")
        {
            StartCoroutine(RotateRoom(1));
        }
        else if (edgeTag == "LeftEdge")
        {
            StartCoroutine(RotateRoom(-1));
        }
    }

    private IEnumerator RotateRoom(int direction)
    {
        isRotating = true;
        
        playerController.canMove = false;
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        playerRb.linearVelocity = Vector3.zero;
        playerRb.isKinematic = true;

        player.SetParent(roomPivot);
        Quaternion startRotation = roomPivot.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 90 * direction, 0);
        float timeElapsed = 0;
        while (timeElapsed < rotationDuration)
        {
            roomPivot.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        roomPivot.rotation = targetRotation;

        player.SetParent(null);
        player.rotation = Quaternion.identity;
        float finalX = direction == 1 ? -38f : 38f;
        player.position = new Vector3(finalX, player.position.y, -41f);

        playerRb.isKinematic = false;
        playerController.canMove = true;
        isRotating = false;

        isCooldown = true;
        yield return new WaitForSeconds(0.2f);
        isCooldown = false;
    
    }
}