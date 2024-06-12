using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    internal static int i_keys;
    internal static int i_mana;
    internal static bool[] b_spellCardObtained = new bool[100];
    internal static int[] i_spell_Cards_Obtained = new int[100];
    internal static int[] i_spell_Card_Level = new int[100];
    internal static int[] i_awakening_Card_Level = new int[100];
    internal static bool[] b_canAwake = new bool[100];
    internal static int[] i_CardSlotNumber = new int[8];
    internal static int i_maxRound;

    internal static bool b_firstDataSave;

    #region Elemental Runes
    internal static int[] i_elementalRunesInStock = new int[100];
    #endregion

    #region Shop Data
    // Spells available in shop
    internal static bool[] b_spellBoughtAlready = new bool[3];
    internal static ShopBuyableSpells[] shopBuyableSpells = new ShopBuyableSpells[3];
    internal static ElementalRunes_SO[] shopBuyableERunes = new ElementalRunes_SO[3];
    internal static int[] i_elementalRunesPurchasesLeftInShop = new int[3];
    #endregion

    #region Date and Time
    internal static int i_lastDayLogInShop;
    #endregion
}
