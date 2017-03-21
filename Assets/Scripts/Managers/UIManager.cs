using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const string PLAYER_HUD_TAG = "PlayerHUD";

    private MagicManager magicManager;

    private Image playerPortrait;
    private RectTransform healthBar;
    private RectTransform focusBar;
    private Image readySpell;
    private RectTransform[] inputStringPanel;

    // Use this for initialization
    void Start()
    {
        var UIExposer = GameObject.FindGameObjectWithTag(PLAYER_HUD_TAG).GetComponent<UIExposer>();
        playerPortrait = UIExposer.playerPortrait.GetComponent<Image>();
        healthBar = UIExposer.healthBar;
        focusBar = UIExposer.focusBar;
        readySpell = UIExposer.readySpell.GetComponent<Image>();
        magicManager = this.GetComponent<MagicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //This could be replaced by much more efficient events -- hook them up to the Managers
        //This is likely done by making the UI manager methods public and removing the Update method.
        //Then the appropriate Manager creates events for those properties, and assigns these methods
        //to the event and updates the UI with a single call instead of per frame.
        UISetPlayerPortrait();
        UISetPlayerResources();
    }

    #region StatusUI

    public void UISetPlayerPortrait()
    {
        //throw new NotImplementedException();
    }

    public void UISetPlayerResources()
    {
        //throw new NotImplementedException();
    }

    #endregion

    #region MagicUI

    public void UISetReadySpellIcon()
    {
        if (magicManager.readySpellPrefab != null && readySpell.sprite != magicManager.readySpellPrefab.GetComponent<Spell>().spellIcon)
        {
            readySpell.gameObject.SetActive(true);
            readySpell.sprite = magicManager.readySpellPrefab.GetComponent<Spell>().spellIcon;
        }

        if (magicManager.readySpellPrefab == null)
        {
            readySpell.gameObject.SetActive(false);
            readySpell.sprite = null;
        }
    }

    #endregion
}
