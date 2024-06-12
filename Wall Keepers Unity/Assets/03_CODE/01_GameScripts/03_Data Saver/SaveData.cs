using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    #region Singleton
    public static SaveData instance { get; private set; } // Can be read by all scripts but only modified by this
    #endregion

    private Container playerData_Container = new Container();

    #region Unity Life Cycle Methods
    private void Awake()
    {
        // Implement Singleton Pattern
        if (instance != null) // Error check to avoid more than one Instance of this script to be created
        {
            Debug.LogError("Tere is more than one LibraryShopManager! " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this; // Equal the instance to this script

        //Reset(); // Only when we want to delete data
        // Load Player Saved Data
        LoadAndAssignData();

        // Only for testing
        PlayerData.i_mana = 100000;
    }
    #endregion

    #region Save and Load Data Methods
    public void AssignDataAndSave()
    {
        // Get data from PlayerData
        playerData_Container.ContainerGetDataFromPlayerData();
        // Save data to Json
        SaveToJson();
    }
    public void SaveToJson()
    {
        string playerData = JsonUtility.ToJson(playerData_Container);
        string filepath = Application.persistentDataPath + "/playerData.json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, playerData);
        PlayerData.b_firstDataSave = true;
        Debug.Log("Data saved");
    }
    public void LoadAndAssignData()
    {
        // Load saved data
        LoadFromJson();
        // Pass saved data to PlayerData
        playerData_Container.ContainerPassDataToPlayerData();

    }
    public void LoadFromJson()
    {
        string filepath = Application.persistentDataPath + "/playerData.json";
        string playerData = System.IO.File.ReadAllText(filepath);

        playerData_Container = JsonUtility.FromJson<Container>(playerData);
        Debug.Log("Data Loaded");
    }

    #endregion

    #region Reset Data
    // Call first to reset Data
    public void Reset()
    {
        SaveToJson();
    }
    #endregion


}


#region Data Container
[System.Serializable]
public class Container
{
    // This Script is used to store data from player
    #region Player Data Container
    public int i_mana= 0;
    public int i_keys = 0;
    public bool[] b_spellCardObtained = new bool[100];
    public int[] i_spell_Cards_Obtained = new int[100];
    public int[] i_spell_Card_Level = new int[100];
    public  int[] i_awakening_Card_Level = new int[100];
    public GameObject[] deck_GameObjects = new GameObject[8];
    public int[] i_CardSlotNumber = new int[8];
    public bool b_isDeckFull;
    public int i_totalCardsInDeck;
    public int i_maxRound;
    public bool b_firstDataSave;
    public bool[] b_CanAwake = new bool[100];

    #region Elemental Runes
    public int[] i_elementalRunesInStock = new int[100];
    #endregion

    #region Shop Data
    public bool[] b_spellBoughtAlready = new bool[3];
    public ShopBuyableSpells[] shopBuyableSpells = new ShopBuyableSpells[3];
    public ElementalRunes_SO[] shopBuyableERunes = new ElementalRunes_SO[3];
    public int[] i_elementalRunesPurchasesLeftInShop = new int[3];

    #endregion

    #region Date and Time
    public int i_lastDayLogInShop = -1;
    #endregion
    #endregion

    internal void ContainerGetDataFromPlayerData() // Before saving data from container we get the data updated from Player Data
    {
        // Get updated data from player Data
        i_mana = PlayerData.i_mana;
        i_keys = PlayerData.i_keys;
        b_spellCardObtained = PlayerData.b_spellCardObtained;
        i_spell_Cards_Obtained = PlayerData.i_spell_Cards_Obtained;
        //deck_GameObjects = PlayerData.deck_GameObjects;
        i_spell_Card_Level = PlayerData.i_spell_Card_Level;
        i_awakening_Card_Level = PlayerData.i_awakening_Card_Level;
        i_CardSlotNumber = PlayerData.i_CardSlotNumber;
        i_maxRound = PlayerData.i_maxRound;
        b_firstDataSave = PlayerData.b_firstDataSave;
        b_CanAwake = PlayerData.b_canAwake;

        #region Elemental Runes
        i_elementalRunesInStock = PlayerData.i_elementalRunesInStock;
        #endregion

        #region Shop Data
        b_spellBoughtAlready = PlayerData.b_spellBoughtAlready;
        shopBuyableSpells = PlayerData.shopBuyableSpells;
        shopBuyableERunes = PlayerData.shopBuyableERunes;
        i_elementalRunesPurchasesLeftInShop = PlayerData.i_elementalRunesPurchasesLeftInShop;
        #endregion

        #region Date and Time
        i_lastDayLogInShop = PlayerData.i_lastDayLogInShop;
        #endregion
    }
    internal void ContainerPassDataToPlayerData()
    {
        PlayerData.i_maxRound= i_maxRound;
        PlayerData.i_mana = i_mana;
        PlayerData.i_keys = i_keys;
        PlayerData.b_spellCardObtained= b_spellCardObtained;
        //PlayerData.deck_GameObjects = deck_GameObjects;
        PlayerData.i_spell_Card_Level = i_spell_Card_Level;
        PlayerData.i_awakening_Card_Level = i_awakening_Card_Level;
        PlayerData.i_spell_Cards_Obtained = i_spell_Cards_Obtained;
        PlayerData.i_CardSlotNumber = i_CardSlotNumber;
        PlayerData.b_firstDataSave= b_firstDataSave;
        PlayerData.b_canAwake = b_CanAwake;

        #region Elemental Runes
        PlayerData.i_elementalRunesInStock = i_elementalRunesInStock;
        #endregion

        #region Shop Data
        PlayerData.b_spellBoughtAlready = b_spellBoughtAlready;
        PlayerData.shopBuyableSpells = shopBuyableSpells;
        PlayerData.shopBuyableERunes = shopBuyableERunes;
        PlayerData.i_elementalRunesPurchasesLeftInShop = i_elementalRunesPurchasesLeftInShop;
        #endregion

        #region Date and Time
        PlayerData.i_lastDayLogInShop = i_lastDayLogInShop;
        #endregion
    }


}
#endregion




