using UnityEngine;

public class M4A1Controller : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    [Header("M4A1 normal rate 800")]
    public int fireRate;
    
    [Header("Normal coefficient 25")]
    public int fireCoefficient;
    
    [Header("Normal speed 20")]
    public float bulletSpeed;
    
    private float fireDelay;
    private float charge;
    private AudioSource audioSource;
    private Vector3 aim;
    private bool mouseMovementTracking;
    
    // Start is called before the first frame update
    void Start()
    {
        fireDelay = 60f * 60f / fireRate / fireCoefficient;
        audioSource = GetComponent<AudioSource>();
        mouseMovementTracking = true;
        charge = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = new Vector3();
        
        if (mouseMovementTracking)
        {
            mousePos = Input.mousePosition;
            mousePos += Camera.main.transform.forward * 10f;
            aim = Camera.main.ScreenToWorldPoint(mousePos);
            // gun.transform.LookAt(aim);
            gameObject.transform.parent.gameObject.transform.LookAt(aim);
        }

        if (Input.GetButton("Fire1")) {
            charge += Time.deltaTime;
            
            if (charge > fireDelay)
            {
                Fire();
                charge = 0.0f;
            }
        }
        else
        {
            charge = 0.0f;
        }

        if (Input.GetButton("Cancel"))
        {
            mouseMovementTracking = false;
        }
    }

    void Fire()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        audioSource.Play();
    }
}
