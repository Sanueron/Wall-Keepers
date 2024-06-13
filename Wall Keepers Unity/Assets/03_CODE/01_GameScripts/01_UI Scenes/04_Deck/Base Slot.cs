using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSlot : MonoBehaviour
{
    // Holds Common variables for Deck slot and Spell Slot

    #region Slots Common Variables
    // Spell Data
    [SerializeField] protected ScripteableCard spellBaseData;
    [SerializeField] protected GameObject spellPrefab;

    // Slot Spell Art
    [SerializeField] protected Image img_spellInSlotVisual;

    // Slot Buttons
    [SerializeField] protected Button button_addOrRemoveSpellToFromDeck;
    [SerializeField] protected Button button_showSpellInfo;
    #endregion

    #region Access methods
    internal ScripteableCard GetSlotSpellInfo()
    {
        return spellBaseData;
    }
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        DeckManager.instance.OnSelectedSlotChanged += DeckManager_OnSelectedSlotChanged;
    }

    #region Events Methods
    private void DeckManager_OnSelectedSlotChanged(object sender, EventArgs e)
    {
        print("Event method");
        if(DeckManager.instance.currentSelectedSlot != this)
        {
            print("Not this");
            HideButtons();
        }
    }
    #endregion

    #region Buttons Methods
    public void CheckIfShowOrHideButtons()
    {
        DeckManager.instance.FireEventOnSlotSelectedChanged();

        if (button_addOrRemoveSpellToFromDeck.gameObject.activeSelf) // Button already active
        {
            HideButtons();
        }
        else // Button not active
        {
            ShowButtons();
        }

    }
    public void ShowButtons()
    {
        // Mark Slot as Current Selected
        DeckManager.instance.currentSelectedSlot = this;
        // Show Buttons
        button_addOrRemoveSpellToFromDeck.gameObject.SetActive(true);
        button_showSpellInfo.gameObject.SetActive(true);
    }
    public void HideButtons()
    {
        // Mark as current selected null
        DeckManager.instance.currentSelectedSlot = null;

        // Hide Buttons
        button_addOrRemoveSpellToFromDeck.gameObject.SetActive(false);
        button_showSpellInfo.gameObject.SetActive(false);
    }
    #endregion


    #endregion
}
