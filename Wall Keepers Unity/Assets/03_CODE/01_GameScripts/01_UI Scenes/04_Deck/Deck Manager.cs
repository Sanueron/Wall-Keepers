using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    #region Spell Info Panel
    [Header("Spell Info Panel Variables")]
    [SerializeField] private Image img_selectedSpellCard;
    [SerializeField] private Image img_smallCard_RequiredCostInFillBar;
    [SerializeField] private TextMeshProUGUI tmp_spellDescription;
    [Header("Spell Stats Variables")]
    [SerializeField] private GameObject go_spellDamageStats;
    [SerializeField] private GameObject go_spellResistanceStats, go_spellSpeedStats, go_spellDurationStats;
    [SerializeField]
    private TextMeshProUGUI tmp_spellStar, tmp_spellLevel, tmp_damageAlone, tmp_damageWithImprovement, tmp_damageImprovement,
        tmp_resistanceAlone, tmp_resistanceWithImprovement, tmp_resistanceImprovement,
        tmp_speedAlone, tmp_speedWithImprovement, tmp_speedImprovement,
        tmp_durationAlone, tmp_durationWithImprovement, tmp_durationImprovement;
    [Header("Spell Level Up and Awakening")]
    [SerializeField] private GameObject go_levelUpItems;
    #endregion

    #region Spell level Up and Awaken Variables
    int i_manaEnergyCost;
    int i_spellCardsCost;
    int i_elementalRunesCost;
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
    private void Start()
    {
        AssignSpellsInDeckAtStart();
    }

    #region Events
    internal void FireEventOnSlotSelectedChanged()
    {
        OnSelectedSlotChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region Unselect Selected Slot
    public void UnselectSlot()
    {
        if(currentSelectedSlot != null)
        {
            currentSelectedSlot.HideButtons();
            currentSelectedSlot = null;
        }
    }
    #endregion

    #region Deck Slots Control
    private void AssignSpellsInDeckAtStart()
    {
        // Assign saved Spells in deck to their current slots
        for (int i = 0; i < deckSlots_array.Length; i++)
        {
            if(PlayerData.spellsInDeckData[deckSlots_array[i].i_deckSlotIndex] != null)
            {
                deckSlots_array[i].SetSpellInfo(PlayerData.spellsInDeckData[deckSlots_array[i].i_deckSlotIndex]);
                deckSlots_array[i].spellPrefab = PlayerData.spellsInDeckData[deckSlots_array[i].i_deckSlotIndex].spellPrefab;
                deckSlots_array[i].img_spellInSlotVisual.sprite = PlayerData.spellsInDeckData[deckSlots_array[i].i_deckSlotIndex]._card_Sprite;
            }
        }
    }
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
                PlayerData.spellsInDeckData[deckSlots_array[i].i_deckSlotIndex] = spellData;
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

        // Save data
        SaveData.instance.AssignDataAndSave();
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

    #region Spell Info Panel Methods
    public void OpenSpellInfoPanel(ScripteableCard spellInfo)
    {
        // Assign Spell Card Sprite and descriptio
        img_selectedSpellCard.sprite = spellInfo._card_Sprite;
        tmp_spellDescription.text = spellInfo.Description;
        // Locally save spell element for later animations when levelling up or awakening.

        // Get Spell Level
        int spellID = spellInfo.i_id;
        int i_spellLevel = PlayerData.i_spell_Card_Level[spellID];
        int i_spellAwakeningLvl = PlayerData.i_awakening_Card_Level[spellID];

        #region Assign Spell Stats
        int spellStar = spellInfo.i_spellStarsTier;
        float f_damage = spellInfo.f_dmg + 
            SpellsData.f_damageIncreaseByLevel * i_spellLevel +
            SpellsData.f_damageIncreaseByAwakening*i_spellAwakeningLvl;
        float f_resistance = spellInfo.f_resistance + 
            SpellsData.f_resistanceIncreaseByLevel * i_spellLevel +
            SpellsData.f_resistanceIncreaseByAwakening * i_spellAwakeningLvl;
        float f_speed = spellInfo.f_projectileSpeed +
            SpellsData.f_speedIncreaseByLevel* i_spellLevel +
            SpellsData.f_speedIncreaseByAwakening * i_spellAwakeningLvl; // if 0 is instant casting
        float f_duration = spellInfo.f_duration +
            SpellsData.f_durationIncreaseByLevel * i_spellLevel +
            SpellsData.f_damageIncreaseByAwakening * i_spellAwakeningLvl;
        #endregion

        // Disactive not applicable stats (if base stat is 0 disactivate)
        if(f_damage > 0) { go_spellDamageStats.SetActive(true); } else {  go_spellDamageStats.SetActive(false);}
        if(f_resistance > 0) { go_spellResistanceStats.SetActive(true); } else { go_spellResistanceStats.SetActive(false); }
        if(f_speed > 0) { go_spellSpeedStats.SetActive(true);} else {  go_spellSpeedStats.SetActive(false); }
        if(f_duration > 0) { go_spellDurationStats.SetActive(true); } else { go_spellDurationStats.SetActive(false); }

        // Level Up and Awakening Costs (Both in Mana and Required items)
        int manaEnergyCostPerlevel = SpellsData.i_spell_ManaEnergy_Cost_Increase_Per_Level;
        i_manaEnergyCost = manaEnergyCostPerlevel + i_spellLevel * manaEnergyCostPerlevel; // Level up and Awakening have the same Mana energy cost
        i_spellCardsCost = SpellsData.i_spell_Cards_Cost_Increase_Per_Level + SpellsData.i_spell_Cards_Cost_Increase_Per_Level * i_spellLevel;
        i_elementalRunesCost = SpellsData.i_elemetalRunes_Cost_Increase_Per_Level+ 
            i_spellAwakeningLvl * SpellsData.i_elemetalRunes_Cost_Increase_Per_Level;

        // Check if spell ready for Awakening - Level Cap reached and Awakening level not increased yet
        // At level 10 - Awk level 1 and so on
        if (i_spellLevel == 10)
        {
            if(i_spellAwakeningLvl == 0)
            {
                // Show Awakening options
            }
        }
        else if (i_spellLevel == 20)
        {
            if (i_spellAwakeningLvl == 1)
            {
                // Show Awakening options
            }
        }
        else if(i_spellLevel == 30)
        {
            if (i_spellAwakeningLvl == 2)
            {
                // Show Awakening options
            }
        }
        else if (i_spellLevel == 40)
        {
            if (i_spellAwakeningLvl == 3)
            {
                // Show Awakening options
            }
        }
        else if (i_spellLevel == 50)
        {
            if (i_spellAwakeningLvl == 4)
            {
                // Show Awakening options
            }
        }
        else // Spell Not ready for Awakening Show Level Up options
        {
            // Check if Spell is fusion or not and Show level up buttons
            switch (spellInfo.e_spellType)
            {
                case ScripteableCard.SpellType.Normal:
                    // Get Spells of this type in stock
                    // Get Spells required for next Level
                    // Assign Spell Card to Spell Required icon in bar
                    img_smallCard_RequiredCostInFillBar.sprite = spellInfo._card_Sprite;
                    // Show Spell Fill bar

                    // Check if enough for Level Up
                    break;
                case ScripteableCard.SpellType.Fusion:
                    break;
            }
            // Set Mana Energy Cost for level Up and Check if enough for level up
        }
        // Update text variables
    }
    #endregion
}
