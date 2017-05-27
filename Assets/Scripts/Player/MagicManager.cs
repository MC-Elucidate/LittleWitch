using System;
using System.Text;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public SpellBooleanPair[] spellList;
    public Transform readySpellPrefab = null;

    private const bool ALL_SPELLS_UNLOCKED = true;
    private const int MAX_INPUTS_LENGTH = 8;
    private const float INPUTS_CLEAR_TIME = 2.5f;

    private UIManager uiManager;
    private StringBuilder inputString;
    private Transform spellSource;
    private Transform cameraTransform;
    private float lastInputTime = 0f;

    public void Start()
    {
        uiManager = this.gameObject.GetComponent<UIManager>();
        inputString = new StringBuilder();
        spellSource = this.gameObject.FindObjectInChildren("SpellSource").transform;
        cameraTransform = Camera.main.transform;
        uiManager.UISetReadySpellIcon();
    }

    public void Update()
    {
        HandleSpellInputs();
        CheckValidSpellInputs();
    }

    public void CastSpell()
    {
        //Casting logic and animations go over here probably
        if (readySpellPrefab != null)
        {
            GameObject.Instantiate(readySpellPrefab, spellSource.position, cameraTransform.rotation);
            readySpellPrefab = null;
            uiManager.UISetReadySpellIcon();
        }
        else
        {
            Debug.Log("No spell is ready");
            //Play no magic spell sound
        }
    }

    public void AddToInputString(int element)
    {
        lastInputTime = 0f;
        inputString.Append(element);

        if (inputString.Length >= MAX_INPUTS_LENGTH)
            //Do we let them keep adding inputs until they get something right? Or clear it if they fill up the input list
            //inputString.Remove(0, inputString.Length - MAX_INPUTS_LENGTH);
            ClearInputs();

        Debug.Log(String.Format("Element{0} Pressed! Combo now: {1}", element, inputString));
    }

    public void ClearInputs()
    {
        inputString.Remove(0, inputString.Length);
    }

    private void CheckValidSpellInputs()
    {
        foreach (SpellBooleanPair pair in spellList)
        {
            if (inputString.ToString().Contains(pair.spellPrefab.GetComponent<Spell>().inputString) && (pair.unlocked || ALL_SPELLS_UNLOCKED))
            {
                readySpellPrefab = pair.spellPrefab;
                uiManager.UISetReadySpellIcon();
                ClearInputs();
            }
        }
    }

    private void HandleSpellInputs()
    {
        if (inputString.Length > 0)
            lastInputTime += Time.deltaTime;
        else
            lastInputTime = 0f;

        if (lastInputTime >= INPUTS_CLEAR_TIME)
        {
            ClearInputs();
            lastInputTime = 0f;
        }
    }

    [Serializable]
    public class SpellBooleanPair
    {
        public Transform spellPrefab;
        public bool unlocked;
    }
}
