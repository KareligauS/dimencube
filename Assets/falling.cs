using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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
            float top = GetComponent<Collider>().bounds.max.y;
            float impactPoint = collision.contacts[0].point.y;
            if (impactPoint >= top - 0.2f)
            {
                isTriggered = true;
                StartCoroutine(FallAndResp());
            }
            
        }
    }

    private IEnumerator FallAndResp()
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
        yield return new WaitForSeconds(respawnTime);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.localPosition = startPos;
        transform.localRotation = startRotation;
        isTriggered = false;
    }

}