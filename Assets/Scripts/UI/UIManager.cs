using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject crosshairObject;
    
    private MagicManager magicManager;
    private PlayerStatus playerStatus;

    private Image playerPortrait;
    private RectTransform healthBar;
    private RectTransform focusBar;
    private Transform crosshair;
    private Image readySpell;
    private RectTransform[] inputStringPanel;

    private int playerHealth = 0, playerMana = 0;

    void Start()
    {
        var UIExposer = GameObject.FindGameObjectWithTag(Helpers.Tags.PlayerHUD).GetComponent<UIExposer>();
        playerPortrait = UIExposer.playerPortrait.GetComponent<Image>();
        healthBar = UIExposer.healthBar;
        focusBar = UIExposer.focusBar;
        readySpell = UIExposer.readySpell.GetComponent<Image>();
        magicManager = this.GetComponent<MagicManager>();
        playerStatus = this.GetComponent<PlayerStatus>();
        crosshair = Instantiate<GameObject>(crosshairObject).transform;

        ToggleCrosshair(false);
    }
    
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
        //If change in player health percentage (discrete states)
        //Change portrait to injured face
    }

    public void UISetPlayerResources()
    {
        if (playerStatus.Health != playerHealth)
        {
            playerHealth = playerStatus.Health;
            this.healthBar.anchorMax = new Vector2((float)playerStatus.Health / PlayerStatus.MaxHealth, 1f);
            this.healthBar.offsetMax = Vector2.zero;
            this.healthBar.offsetMin = Vector2.zero;
        }
        if (playerStatus.Mana != playerMana)
        {
            playerMana = playerStatus.Mana;
            this.focusBar.anchorMax = new Vector2((float)playerStatus.Mana / PlayerStatus.MaxMana, 1f);
            this.focusBar.offsetMax = Vector2.zero;
            this.focusBar.offsetMin = Vector2.zero;
        }
        //for (int i = 0; i > Hearts; i++)
        //{
        //    int col = i % 9;
        //    int row = Mathf.Floor(i / 9f);

        //    GUI.DrawTexture(new Rect(10 + (74 * col), 10 + (74 * row), 64, 64), heartTexture, ScaleMode.ScaleToFit);
        //}
    }

    #endregion

    #region MagicUI

    public void UISetReadySpellIcon()
    {
        if (magicManager == null)
            return;

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

    internal void ToggleCrosshair(bool toggled)
    {
        crosshair.gameObject.SetActive(toggled);
    }

    #endregion
}
