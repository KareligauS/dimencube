using System.Collections;
using UnityEngine;

public class falling : MonoBehaviour
{
    public float delay = 0.5f;
    private Rigidbody rb;
    private float respawnTime = 3f;
    private Vector3 startPos;
    private Quaternion startRotation;
    private bool isTriggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; //freeze
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(FallAndResp());
        }
    }

    private IEnumerator FallAndResp()
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
        yield return new WaitForSeconds(respawnTime);
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPos;
        transform.rotation = startRotation;
        isTriggered = false;
    }

}