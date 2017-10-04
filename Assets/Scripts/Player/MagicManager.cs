using System;
using System.Text;
using UnityEngine;

public class MagicManager : MonoBehaviour
{

    private FireMode fireMode = null;
    private WindMode windMode = null;
    private ASpellMode activeSpellMode = null;

    private UIManager uiManager;
    private Transform spellSource;
    private CameraScript cameraManager;

    public void Start()
    {
        uiManager = this.gameObject.GetComponent<UIManager>();
        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
        fireMode = gameObject.GetComponentInChildren<FireMode>();
        windMode = gameObject.GetComponentInChildren<WindMode>();
        activeSpellMode = fireMode;

        cameraManager = Camera.main.GetComponent<CameraScript>();
        uiManager.UISetReadySpellIcon();
    }

    public void Update()
    {
    }

    public void BasicAttackPressed()
    {
        if(cameraManager.state == CameraScript.CameraMode.Free)
            activeSpellMode.AttackPressed(spellSource.position, transform.forward);
        else if (cameraManager.state == CameraScript.CameraMode.Aim)
            activeSpellMode.AttackPressed(spellSource.position, uiManager.crosshair.position - spellSource.position, uiManager.crosshair.position);
    }

    public void BasicAttackReleased()
    {
        if (cameraManager.state == CameraScript.CameraMode.Free)
            activeSpellMode.AttackReleased(spellSource.position, transform.forward);
        else if (cameraManager.state == CameraScript.CameraMode.Aim)
            activeSpellMode.AttackReleased(spellSource.position, uiManager.crosshair.position - spellSource.position, uiManager.crosshair.position);
    }

    public Sprite GetSpellIcon()
    {
        return activeSpellMode.spellIcon;
    }

    public void ActivateFireMode()
    {
        if(activeSpellMode != fireMode)
        activeSpellMode = fireMode;
    }

    public void ActivateWindMode()
    {
        if (activeSpellMode != windMode)
            activeSpellMode = windMode;
    }
}
