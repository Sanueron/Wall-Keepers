using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerData.i_mana = 10000;
        PlayerData.i_spell_Cards_Obtained[1] = 50;
        PlayerData.i_spell_Card_Level[1] = 10;
    }

}
