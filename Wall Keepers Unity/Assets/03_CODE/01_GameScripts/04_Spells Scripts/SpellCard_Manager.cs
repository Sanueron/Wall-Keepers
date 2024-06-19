using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpellCard_Manager : MonoBehaviour
{
    public Vector3 v3_cardInitialPosition;
    [SerializeField] private ScripteableCard card_Data;
    
    private void Start()
    {
        InitializedCard();
    }
    private void InitializedCard()
    {
        v3_cardInitialPosition = transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = card_Data._card_Sprite;
    }

    public ScripteableCard GetCardData()
    {
        return card_Data;   

    }
    
}
