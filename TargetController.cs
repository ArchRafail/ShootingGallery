using System.Collections;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject uiTargetHitInfo;
    
    [Header("Normal speed 1")]
    public float movementSpeed;
    [Header("Optimal value 4")]
    public float rotationTime;
    [Header("Best state - unchecked")]
    public bool stopWhenHide;

    private float DELAY_FOR_TEXT_SHOWING = 1.5f;
    private float SAFE_EXTREME_CLEARENCE = 0.7f;
    private float extremeMoveDistance;
    private Vector3 targetPosition;
    private bool rightMove;
    private GameObject rail;
    
    private bool hided;
    public bool Hided
    {
        get { return hided; }
        set { hided = value; }
    }
    
    private bool isRotating;
    public bool IsRotating
    {
        get { return isRotating; }
        set { isRotating = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiTargetHitInfo.SetActive(false);
        var rightMostObject = GameObject.Find("RightWall");
        var halfWidthOfArea = rightMostObject.transform.position.x - (rightMostObject.transform.localScale.x / 2);
        extremeMoveDistance = halfWidthOfArea - SAFE_EXTREME_CLEARENCE;
        rightMove = true;
        rail = gameObject.transform.parent.gameObject;
        hided = false;
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = gameObject.transform.position;
        var movementByHided = movementSpeed;
        if (stopWhenHide)
        {
            movementByHided = hided ? 0 : movementSpeed;
        }
        if (rightMove)
        {
            if (targetPosition.x < extremeMoveDistance)
            {
                gameObject.transform.position = new Vector3(targetPosition.x + movementByHided * Time.deltaTime, targetPosition.y,
                    targetPosition.z);
            }
            else
            {
                rightMove = false;
            }
        }
        else
        {
            if (targetPosition.x > extremeMoveDistance * -1)
            {
                gameObject.transform.position = new Vector3(targetPosition.x - movementByHided * Time.deltaTime, targetPosition.y,
                    targetPosition.z);
            }
            else
            {
                rightMove = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        Destroy(other.gameObject);
        OnHit();
    }

    private void OnHit()
    {
        if (!hided)
        {
            StartCoroutine(StartRotation(-90, rotationTime));
            uiTargetHitInfo.SetActive(true);
            StartCoroutine(HitInfoShow());
            hided = true;
        }
    }

    public IEnumerator StartRotation(float degrees, float rotationTime)
    {
        if(isRotating) yield break;
        isRotating = true;
    
        var passedTime = 0f;
        var startRotation = rail.transform.rotation;
        var targetRotation = rail.transform.rotation * Quaternion.Euler(degrees, 0, 0);
    
        while(passedTime < rotationTime)
        {
            var lerpFactor = passedTime / rotationTime;
            rail.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, lerpFactor);
            passedTime += Mathf.Min(rotationTime - passedTime, Time.deltaTime);
            yield return null;
        }
        
        rail.transform.rotation = targetRotation;
        isRotating = false;
    }

    private IEnumerator HitInfoShow()
    {
        yield return new WaitForSeconds(DELAY_FOR_TEXT_SHOWING);
        uiTargetHitInfo.SetActive(false);
    }

}
