using UnityEngine;

public class pickup : MonoBehaviour
{
    private manager m;
    void Start()
    {
        m = FindFirstObjectByType<manager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m.CollectKey();
            Destroy(gameObject);
        }
    }
}
