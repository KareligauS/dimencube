using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private manager m;
    private bool isActive = false;
    void Start()
    {
        m = FindFirstObjectByType<manager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !isActive)
        {
            isActive = true;
            m.UpdateSpawnPoint(collider.transform.position, m.roomPivot.rotation);
        }
    }

}
