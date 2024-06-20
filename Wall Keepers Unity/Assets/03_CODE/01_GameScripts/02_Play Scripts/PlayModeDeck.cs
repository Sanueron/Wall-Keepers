using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayModeDeck : MonoBehaviour
{
    #region Variables
    //Deck cards --> Player's cards 
    //Hand cards --> Playable cards

    [SerializeField] private List<GameObject> cards_InHand_GameObjects;
    [SerializeField] private List<GameObject> cards_NotInHand_GameObjects;
    [SerializeField] private  List<GameObject> allCardsList_GameObjects;
    [SerializeField] private List<Transform> handPositions_Transform;
    public static PlayModeDeck instance { get; private set; }
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

        ProvisionalSaveCards();
        GetDeck();


    }
    private void Start()
    {

        GetFirstHand();

    }
    #endregion

    #region Deck Methods
    //Delete when the DeckScene its ready 
    private void ProvisionalSaveCards()
    {

        for (int i = 0; i < PlayerData.i_CardSlotNumber.Length; ++i)
        {

            PlayerData.i_CardSlotNumber[i] = Random.Range(1, 6);


        }
        SaveData.instance.AssignDataAndSave();

    }

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
                        GameObject obj = Instantiate(allCardsList_GameObjects[PlayerData.i_CardSlotNumber[i]], gameObject.transform);
                        obj.name = obj.name + r;
                        obj.SetActive(false);
                        cards_NotInHand_GameObjects.Add(obj);
                    }
                    else
                    {
                        GameObject obj = Instantiate(allCardsList_GameObjects[PlayerData.i_CardSlotNumber[i]], gameObject.transform);
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
    public void GetSpellBackToDeck(SpellCard_Manager  card)
    {
        int cardListIndex = cards_InHand_GameObjects.IndexOf(card.gameObject);
        cards_NotInHand_GameObjects.Add(cards_InHand_GameObjects[cardListIndex]);  
        cards_InHand_GameObjects.RemoveAt(cardListIndex);


    }

    #endregion
}
