using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform roomPivot;
    public Transform player;
    public PlayerController playerController;
    public float rotationDuration = 0.5f;

    private bool isRotating = false;

    void Update()
    {
        // testing
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating) StartCoroutine(RotateRoom(1));
        if (Input.GetKeyDown(KeyCode.E) && !isRotating) StartCoroutine(RotateRoom(-1));
    }

    public void TriggerEdge(string edgeTag)
    {
        if (isRotating) return;
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

        float teleportX = direction == 1 ? -3.5f : 3.5f;
        player.position = new Vector3(teleportX, player.position.y, player.position.z);
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

        playerRb.isKinematic = false;
        playerController.canMove = true;
        isRotating = false;
    }
}