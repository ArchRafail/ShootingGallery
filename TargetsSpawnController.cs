using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetsSpawnController : MonoBehaviour
{
    public GameObject targetSystem1;
    public GameObject targetSystem2;
    public GameObject targetSystem3;
    public GameObject targetSystem4;
    
    private GameObject[] targetSystems;
    private TargetController[] targetControllers;
    private GameObject[] targets;
    private bool queue;
    private int liveTarget;
    
    private void Awake()
    {
        targetSystems = new [] { targetSystem1, targetSystem2, targetSystem3, targetSystem4 };
        var targetControllersList = new List<TargetController>();
        var targetsList = new List<GameObject>();
        foreach (var targetSystem in targetSystems)
        {
            var rail = targetSystem.transform.Find("Rail").gameObject;
            var target = rail.transform.Find("Target").gameObject;
            targetsList.Add(target);
            targetControllersList.Add(target.GetComponent<TargetController>());
        }
        targets = targetsList.ToArray();
        targetControllers = targetControllersList.ToArray();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        queue = false;
        liveTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            foreach (var target in targets)
            {
                if (target.transform.rotation.x == 0)
                {
                    HideTarget(target);
                }
            }
            if (targets[0].transform.rotation.x != 0)
            {
                ShowTarget(targets[0]);
            }
            queue = true;
            liveTarget = 0;
        }
        
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ShowAllTargets();
            queue = false;
        }

        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
        
        if (queue)
        {
            if (targetControllers[liveTarget].Hided)
            {
                liveTarget += 1;
                liveTarget = liveTarget < targets.Length ? liveTarget : 0;
                ShowTarget(targets[liveTarget]);
            }
        }
        else
        {
            var totalDeadTargets = 0;
            foreach (var targetController in targetControllers)
            {
                if (targetController.Hided && !targetController.IsRotating)
                {
                    totalDeadTargets += 1;
                }
                else
                {
                    break;
                }
            }
            if (totalDeadTargets == targetControllers.Length)
            {
                ShowAllTargets();
            }
        }
    }

    void ShowAllTargets()
    {
        foreach (var target in targets)
        {
            if (target.transform.rotation.x != 0)
            {
                ShowTarget(target);
            }
        }
    }

    void HideTarget(GameObject target)
    {
        var indexOfTarget = Array.IndexOf(targets, target);
        var targetController = targetControllers[indexOfTarget];
        if (!targetController.IsRotating)
        {
            StartCoroutine(targetController.StartRotation(-90, targetController.rotationTime));
            targetController.Hided = true;
        }
    }

    void ShowTarget(GameObject target)
    {
        var indexOfTarget = Array.IndexOf(targets, target);
        var targetController = targetControllers[indexOfTarget];
        if (!targetController.IsRotating)
        {
            StartCoroutine(targetController.StartRotation(-270, targetController.rotationTime/2));
            targetController.Hided = false;
        }
    }

    
    
}
