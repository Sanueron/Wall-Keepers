using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellEffect : MonoBehaviour
{

    internal ScripteableCard card_Data;




    public void SetCardData(ScripteableCard card)
    {

        card_Data = card;   
    }
}
