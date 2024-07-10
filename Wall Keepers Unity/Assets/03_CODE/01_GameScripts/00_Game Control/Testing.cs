using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] PlayMode_DeckManager _deckManager;
    public List<GameObject> testSpell;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerData.i_keys = 0;
        PlayerData.i_mana = 0;
        PlayerData.i_spell_Cards_Obtained[1] = 50;
        PlayerData.i_spell_Cards_Obtained[2] = 50;
        PlayerData.i_spell_Cards_Obtained[3] = 50;
        PlayerData.i_spell_Cards_Obtained[4] = 50;
        PlayerData.i_spell_Cards_Obtained[5] = 50;
        PlayerData.i_spell_Cards_Obtained[6] = 50;
        PlayerData.i_spell_Cards_Obtained[7] = 50;
        PlayerData.i_spell_Cards_Obtained[8] = 50;
        PlayerData.i_spell_Cards_Obtained[9] = 50;
        PlayerData.i_spell_Cards_Obtained[10] = 50;
        PlayerData.i_spell_Cards_Obtained[11] = 50;
        PlayerData.b_spellCardObtained[1] = true;
        PlayerData.b_spellCardObtained[2] = true;
        PlayerData.b_spellCardObtained[3] = true;
        PlayerData.b_spellCardObtained[4] = true;
        PlayerData.b_spellCardObtained[5] = true;
        PlayerData.b_spellCardObtained[6] = true;
        PlayerData.b_spellCardObtained[7] = true;
        PlayerData.b_spellCardObtained[8] = true;
        PlayerData.b_spellCardObtained[9] = true;
        PlayerData.b_spellCardObtained[10] = true;
        PlayerData.b_spellCardObtained[11] = true;
        //PlayerData.i_spell_Card_Level[1] = 9;
        //PlayerData.i_elementalRunesInStock[0] = 5;
        //print(PlayerData.i_lastDayLogInShop);
        //PlayerData.b_spellCardObtained[1] = true;
        //TestDeck();

    }

    public void SmallEnemyHit()
    {
        WallManager.instance.ReceiveHit(200f);

    }

    public void BigEnemyHit()
    {
        WallManager.instance.ReceiveHit(600f);
    }

    public void TestDeck()
    {
        for (int i = 0; i < PlayerData.go_spellsIndeck.Length; i++)
        {
            PlayerData.go_spellsIndeck[i] = testSpell[i];

        }


    }




}
