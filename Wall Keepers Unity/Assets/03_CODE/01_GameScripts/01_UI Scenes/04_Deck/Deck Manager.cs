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
    internal void AssignNextEmptyDeckSlot()
    {
        for (int i = 0; i < deckSlots_array.Length; i++)
        {
            if (deckSlots_array[i].GetSlotSpellInfo() == null)
            {
                // Assign Spell Card
                // Assign Spell Data
                // Assign Spell Prefab
            }
        }

    } 
    #endregion
}
