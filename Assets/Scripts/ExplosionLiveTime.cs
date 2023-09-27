using UnityEngine;

public class ExplosionLiveTime : MonoBehaviour
{
    private void Start()
    {
    Invoke(nameof(DestroyObject), 2f);    
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
