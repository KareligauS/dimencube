using UnityEngine;

public class moving : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(10, 0, 0); //move distance
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.localPosition;
        targetPos = startPos + moveOffset;
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.localPosition = Vector3.Lerp(startPos, targetPos, time);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
