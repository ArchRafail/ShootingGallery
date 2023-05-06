using UnityEngine;

public class DestroyingObjectByTime : MonoBehaviour
{
    public float time;
    
    void Start()
    {
        Destroy(gameObject, time);
    }

    void Update()
    {
        
    }
}
