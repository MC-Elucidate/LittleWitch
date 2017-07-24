using System;
using System.Text;
using UnityEngine;

public class MagicManager : MonoBehaviour
{

    private FireMode fireMode = null;

    private UIManager uiManager;
    private Transform spellSource;

    public void Start()
    {
        uiManager = this.gameObject.GetComponent<UIManager>();
        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
        fireMode = gameObject.GetComponentInChildren<FireMode>();
        uiManager.UISetReadySpellIcon();
    }

    public void Update()
    {
    }

    public void BasicAttackPressed()
    {
        fireMode.BasicAttackPressed(spellSource.position, transform.rotation);
    }

    public Sprite GetSpellIcon()
    {
        return fireMode.spellIcon;
    }
}
