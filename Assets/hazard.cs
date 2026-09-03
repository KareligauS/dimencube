using Unity.VisualScripting;
using UnityEngine;

public class hazard : MonoBehaviour
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
            m.Respawn();
        }
    }
}
