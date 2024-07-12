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
        //PlayerData.b_spellCardObtained[9] = true;
        
        //ObtainAllCards();    
        TestDeck();
        //PlayerData.i_keys = 100;
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
            print(PlayerData.go_spellsIndeck[i]);

        }
    }

    public void ObtainAllCards()
    {

     
            for (int i = 0; i < PlayerData.i_spell_Cards_Obtained.Length; i++)
            {
                    PlayerData.b_spellCardObtained[i] = true;
             }

    }



}
