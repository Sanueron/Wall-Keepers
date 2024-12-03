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
        PlayerData.b_spellCardObtained[35] = true;

        // ObtainAllCards();    
        // TestDeck();
        // PlayerData.i_keys = 1000;
        //PlayerData.i_mana = 1000;

    }
    private void Start()
    {
    }
}
