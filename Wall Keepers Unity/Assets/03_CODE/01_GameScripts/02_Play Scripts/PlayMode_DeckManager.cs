using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayMode_DeckManager : MonoBehaviour
{
    #region Variables
    //Deck cards --> Player's cards 
    //Hand cards --> Playable cards

    [SerializeField] private List<GameObject> cards_InHand_GameObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> cards_NotInHand_GameObjects =new List<GameObject>();
    [SerializeField] private List<Transform> handPositions_Transform = new List<Transform>();
    [SerializeField] private Transform deckPosition_Transform;
    private Vector3 v3_lastcardPosition;

    public static PlayMode_DeckManager instance { get; private set; }
    #endregion

    #region Unity Life Cycle

    private void Awake()
    {
        // Implement Singleton Pattern
        if (instance != null) // Error check to avoid more than one Instance of this script to be created
        {
            Debug.LogError("There is more than one DeckManager! " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    private void Start()
    {
        GetDeck();
        GetFirstHand();

    }
    #endregion

    #region Deck Methods
    //Delete when the DeckScene its ready 
 
    //Generate cards Pool from player's deck 
    private void GetDeck()
    {

        for (int r = 0; r < 3; ++r)
        {
            for (int i = 0; i < PlayerData.i_CardSlotNumber.Length; ++i)
            {
                if (PlayerData.i_CardSlotNumber[i] != 0)
                {

                    if (r <= 0)
                    {
                        GameObject obj = Instantiate(PlayerData.go_spellsIndeck[i], deckPosition_Transform.transform);
                        obj.name = obj.name + r;
                        obj.SetActive(false);
                        cards_NotInHand_GameObjects.Add(obj);
                    }
                    else
                    {
                        GameObject obj = Instantiate(PlayerData.go_spellsIndeck[i], deckPosition_Transform.transform);
                        obj.name = obj.name + r;
                        obj.SetActive(false);
                        if (r >= 2)
                        {
                            cards_NotInHand_GameObjects.Add(obj);

                        }
                        else
                        {
                            cards_NotInHand_GameObjects.Add(obj);

                        }
                    }
                }
                else
                {
                    Debug.Log("Null");
                }
            }
        }


    }
    //Place 5 random cards in Hand
    private void GetFirstHand()
    {
        for (int i = 0; i < 5; ++i)
        {
            int randomIndex = Random.Range(0, cards_NotInHand_GameObjects.Count);
            cards_InHand_GameObjects.Add(cards_NotInHand_GameObjects[randomIndex]);
            cards_NotInHand_GameObjects.RemoveAt(randomIndex);

            cards_InHand_GameObjects[i].SetActive(true);
            cards_InHand_GameObjects[i].gameObject.transform.position = handPositions_Transform[i].position;
        }
    }
    //Still working......
    public void GetSpellBackToDeck(SpellCard_Manager card, Vector3 previousPosition)
    {
        int cardListIndex = cards_InHand_GameObjects.IndexOf(card.gameObject);
        //cards_InHand_GameObjects[cardListIndex].SetActive(false);   
        cards_NotInHand_GameObjects.Add(cards_InHand_GameObjects[cardListIndex]);
        cards_InHand_GameObjects.RemoveAt(cardListIndex);
        GetSpellToHand(previousPosition);
    }
    private void GetSpellToHand( Vector3 peviousPosition)
    {
       
          int randomIndex = Random.Range(0, cards_NotInHand_GameObjects.Count);
          cards_NotInHand_GameObjects[randomIndex].SetActive(true);
         
          cards_NotInHand_GameObjects[randomIndex].transform.position = peviousPosition;
          cards_NotInHand_GameObjects[randomIndex].GetComponent<SpellCard_Manager>().v3_cardInitialPosition = peviousPosition;
          //Cambiar de lista 
           cards_InHand_GameObjects.Add(cards_NotInHand_GameObjects[randomIndex]);
           cards_NotInHand_GameObjects.RemoveAt(randomIndex);

    }
    #endregion
}
