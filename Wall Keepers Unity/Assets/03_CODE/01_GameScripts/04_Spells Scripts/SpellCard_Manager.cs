using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpellCard_Manager : MonoBehaviour
{
    public Vector3 v3_cardInitialPosition;
    [SerializeField] private ScripteableCard card_Data;
    [SerializeField] private GameObject _spellEffect_GameObject;
    
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

   
    public void ActiveEffect(Vector3 starPosition, Vector3 target)
    {
       
        
        ProjectileEffect effect_script = _spellEffect_GameObject.GetComponent<ProjectileEffect>(); 

        effect_script.CastBall(starPosition, target);
        _spellEffect_GameObject.SetActive(true);
    }

    public void DisableCard()
    {


        gameObject.SetActive(false);    
    }

}
