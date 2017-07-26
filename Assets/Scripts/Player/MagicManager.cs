using System;
using System.Text;
using UnityEngine;

public class MagicManager : MonoBehaviour
{

    private FireMode fireMode = null;

    private UIManager uiManager;
    private Transform spellSource;
    private CameraScript cameraManager;

    public void Start()
    {
        uiManager = this.gameObject.GetComponent<UIManager>();
        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
        fireMode = gameObject.GetComponentInChildren<FireMode>();
        cameraManager = Camera.main.GetComponent<CameraScript>();
        uiManager.UISetReadySpellIcon();
    }

    public void Update()
    {
    }

    public void BasicAttackPressed()
    {
        if(cameraManager.state == CameraScript.CameraMode.Free)
            fireMode.BasicAttackPressed(spellSource.position, transform.rotation);
        else if (cameraManager.state == CameraScript.CameraMode.Aim)
            fireMode.BasicAttackPressed(spellSource.position, Quaternion.LookRotation(uiManager.crosshair.position - spellSource.position, Vector3.up));
    }

    public Sprite GetSpellIcon()
    {
        return fireMode.spellIcon;
    }
}
