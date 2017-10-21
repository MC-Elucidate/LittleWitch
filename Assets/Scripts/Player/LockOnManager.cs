using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour {


    private GameObject LockOnIndicator;
    private CameraManager cameraManager;
    private float Radius = 20f;

    [HideInInspector]
    public GameObject LockOnTarget { get; private set; }

    [SerializeField]
    private GameObject LockOnIndicatorPrefab;
    [SerializeField]
    private LayerMask LockOnLayer;

    [ReadOnly]
    [SerializeField]
    private bool SoftLockedOn = false;
    [ReadOnly]
    [SerializeField]
    private bool HardLockedOn = false;
    

    void Start () {
        cameraManager = Camera.main.GetComponent<CameraManager>();
    }
	
	void Update () {
        if (SoftLockedOn && LockOnIndicator == null)
            TurnOffSoftLockOn();

        if (HardLockedOn && LockOnIndicator == null)
            TurnOffHardLockOn();

        if (cameraManager.state == CameraMode.Free)
            CheckSoftLockOn();
        else if (cameraManager.state == CameraMode.Aim && (SoftLockedOn || HardLockedOn))
        {
            TurnOffSoftLockOn();
            TurnOffHardLockOn();
        }
    }

    public void CheckSoftLockOn()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position + transform.forward * Radius, Radius, LockOnLayer);
        Collider closestObject = null;

        if (objectsInRange.Length == 0)
            return;

        float closestObjectDistance = float.MaxValue;
        foreach (Collider objectInRange in objectsInRange)
        {
            if (objectInRange.isTrigger)
                continue;
            float objectDistance = Mathf.Abs((objectInRange.transform.position - transform.position).sqrMagnitude);
            if (objectDistance < closestObjectDistance)
            {
                closestObjectDistance = objectDistance;
                closestObject = objectInRange;
            }
        }

        if (closestObject == null)
            return;

        if (LockOnTarget == null || LockOnTarget.name != closestObject.name)
        {
            TurnOffSoftLockOn();
            TurnOnSoftLockOn(closestObject.gameObject);
            
        }
    }

    private void TurnOffSoftLockOn()
    {
        if (!SoftLockedOn)
            return;
        SoftLockedOn = false;
        Destroy(LockOnIndicator);
        LockOnTarget = null;
    }

    private void TurnOnSoftLockOn(GameObject target)
    {
        LockOnTarget = target;
        SoftLockedOn = true;

        Transform LockOnIndicatorTransform = Helpers.FindObjectInChildren(LockOnTarget, "LockOnIndicatorPosition").transform;
        LockOnIndicator = Instantiate(LockOnIndicatorPrefab, LockOnIndicatorTransform.position, Quaternion.identity);
        LockOnIndicator.transform.parent = LockOnIndicatorTransform;
    }

    public void HardLockOnPressed()
    {
        if (HardLockedOn)
            TurnOffHardLockOn();
        else if (SoftLockedOn)
            TurnOnHardLockOn();
    }

    private void TurnOffHardLockOn()
    {
        if (!HardLockedOn)
            return;
        HardLockedOn = false;
        cameraManager.TurnOffLockOn();
        Destroy(LockOnIndicator);
        LockOnTarget = null;
    }

    private void TurnOnHardLockOn()
    {
        SoftLockedOn = false;
        HardLockedOn = true;
        cameraManager.LockOnToTarget();
    }

    //Draw LockOn Range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * Radius, Radius);
    }
}
