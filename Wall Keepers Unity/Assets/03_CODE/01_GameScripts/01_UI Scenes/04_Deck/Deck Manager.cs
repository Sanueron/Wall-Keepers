using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    #region Singleton
    public static DeckManager instance { get; private set; } // Can be read by all scripts but only modified by this
    #endregion

    #region Events
    public event EventHandler OnSelectedSlotChanged;
    #endregion

    #region Deck Main Variables
    internal BaseSlot currentSelectedSlot; // To know which slot is currently selected
    [Header("Deck Slots")]
    [SerializeField] internal DeckSlot[] deckSlots_array = new DeckSlot[8];
    #endregion


    // Start is called before the first frame update
    void Awake()
    {
        // Implement Singleton Pattern
        if (instance != null) // Error check to avoid more than one Instance of this script to be created
        {
            Debug.LogError("Tere is more than one DeckManager! " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this; // Equal the instance to this script
    }

    #region Events
    internal void FireEventOnSlotSelectedChanged()
    {
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region Deck Slots Control
    internal void AssignNextEmptyDeckSlot(Sprite spr_spellCard, ScripteableCard spellData, GameObject spellPrefab)
    {
        for (int i = 0; i < deckSlots_array.Length; i++)
        {
            if (deckSlots_array[i].GetSlotSpellInfo() == null)
            {
                // Assign Spell Card
                deckSlots_array[i].img_spellInSlotVisual.sprite = spr_spellCard;
                // Assign Spell Data
                deckSlots_array[i].SetSpellInfo(spellData);
                // Assign Spell Prefab
                deckSlots_array[i].spellPrefab = spellPrefab;
                PlayerData.deck_GameObjects[deckSlots_array[i].i_deckSlotIndex] = deckSlots_array[i].spellPrefab;
                break;
            }
        }

        // Check if deck full
        PlayerData.b_isDeckFull = true; // Make true and then check if any slot is empty, if so make false
        for (int i = 0; i < deckSlots_array.Length; i++)
        {
            if (deckSlots_array[i].GetSlotSpellInfo() == null)
            {
                PlayerData.b_isDeckFull = false;
                break;
            }
        }
    } 

    internal bool CheckIfSpellAlreadyInDeck(ScripteableCard spellData)
    {
        for (int i = 0; i < deckSlots_array.Length; i++)
        {
            if (deckSlots_array[i].GetSlotSpellInfo() != null)
            {
                if(deckSlots_array[i].GetSlotSpellInfo() == spellData)
                {
                    return true;
                }
            }
        }

        return false;
    }
    #endregion
}
