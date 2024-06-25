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
        PlayerData.i_keys = 15;
        //PlayerData.i_mana = 10000;
        //PlayerData.i_spell_Cards_Obtained[1] = 50;
        //PlayerData.i_spell_Card_Level[1] = 9;
        //PlayerData.i_elementalRunesInStock[0] = 5;
        //print(PlayerData.i_lastDayLogInShop);
       
        TestDeck();

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
        //_deckManager.cards_NotInHand_GameObjects = testSpell;
        /*
        for (int i = 0; i < PlayerData.go_spellsIndeck.Length; i++)
        {
         
            PlayerData.go_spellsIndeck[i] = testSpell[i];

        }
       */


    }




}
