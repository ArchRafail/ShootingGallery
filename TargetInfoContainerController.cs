using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfoContainerController : MonoBehaviour
{
    private GameObject[] targetsHitInfo;
    private Vector3 targetHitInfoPosition;
        
    // Start is called before the first frame update
    void Start()
    {
        targetsHitInfo = GameObject.FindGameObjectsWithTag("TargetHitInfo");
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = new Vector3();
        mousePos = Input.mousePosition;
        if (mousePos.x > Screen.width/2)
        {
            targetHitInfoPosition = new Vector3(mousePos.x - 60, mousePos.y - 25, -50);
        }
        else
        {
            targetHitInfoPosition = new Vector3(mousePos.x + 80, mousePos.y - 25, -50);
        }
        foreach (var targetHitInfo in targetsHitInfo)
        {
            targetHitInfo.transform.position = targetHitInfoPosition;
        }
    }
}
