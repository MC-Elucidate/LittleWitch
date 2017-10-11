using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject crosshairObject;
    
    private MagicManager magicManager;
    private PlayerStatus playerStatus;

    private Image playerPortrait;
    private RectTransform heartBackgroundsPanel;
    private RectTransform heartsPanel;
    private RectTransform focusBar;
    public Transform crosshair { get;  private set; }
    private Image readySpell;
    private RectTransform[] inputStringPanel;
    private Text gemsText;

    private int playerMaxHealth = 0, playerHealth = 0, playerMana = 0, gems = 0;

    void Start()
    {
        var UIExposer = GameObject.FindGameObjectWithTag(Helpers.Tags.PlayerHUD).GetComponent<UIExposer>();
        playerPortrait = UIExposer.PlayerPortrait.GetComponent<Image>();
        heartBackgroundsPanel = UIExposer.PlayerHeartBackgroundsPanel;
        heartsPanel = UIExposer.PlayerHeartsPanel;
        focusBar = UIExposer.PlayerFocusBar;
        readySpell = UIExposer.PlayerReadySpell.GetComponent<Image>();
        magicManager = this.GetComponent<MagicManager>();
        playerStatus = this.GetComponent<PlayerStatus>();
        crosshair = Instantiate<GameObject>(crosshairObject).transform;
        gemsText = UIExposer.PlayerGemsCounter.GetComponent<Text>();

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
        UISetReadySpellIcon();
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
        if (PlayerStatus.MaxHealth != playerMaxHealth)
        {
            playerMaxHealth = PlayerStatus.MaxHealth;

            for (int heartCount = 0; heartCount < heartBackgroundsPanel.childCount; heartCount++)
            {
                var currentHeart = heartBackgroundsPanel.GetChild(heartCount).gameObject;

                if ((heartCount + 1) <= PlayerStatus.MaxHealth)
                    currentHeart.SetActive(true);
                else
                    currentHeart.SetActive(false);
            }
        }
        if (playerStatus.Health != playerHealth)
        {
            playerHealth = playerStatus.Health;

            for (int heartCount = 0; heartCount < heartsPanel.childCount; heartCount++)
            {
                var currentHeart = heartsPanel.GetChild(heartCount).gameObject;

                if ((heartCount + 1) <= playerStatus.Health)
                    currentHeart.SetActive(true);
                else
                    currentHeart.SetActive(false);
            }
        }

        if (playerStatus.Focus != playerMana)
        {
            playerMana = (int)playerStatus.Focus;
            this.focusBar.localScale = new Vector3((float)playerStatus.Focus / PlayerStatus.MaxFocus, 1f, 1f);
        }

        if (playerStatus.Gems != gems)
        {
            gems = playerStatus.Gems;
            gemsText.text = gems.ToString();
        }
    }

    #endregion

    #region MagicUI

    public void UISetReadySpellIcon()
    {
        if (magicManager == null)
            return;

        if (magicManager.GetSpellIcon() != null && readySpell.sprite != magicManager.GetSpellIcon())
        {
            readySpell.gameObject.SetActive(true);
            readySpell.sprite = magicManager.GetSpellIcon();
        }

        if (magicManager.GetSpellIcon() == null)
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
