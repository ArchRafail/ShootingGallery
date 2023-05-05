using UnityEngine;

public class DestroyTagOnTrigger : MonoBehaviour
{
    public new string tag;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            Destroy(other.gameObject);
        }
    }
}
