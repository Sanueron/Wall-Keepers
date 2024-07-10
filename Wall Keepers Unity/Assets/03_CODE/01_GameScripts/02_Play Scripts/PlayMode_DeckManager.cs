using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayMode_DeckManager : MonoBehaviour
{
    #region Variables
    //Deck cards --> Player's cards 
    //Hand cards --> Playable cards

    [SerializeField] private List<GameObject> cards_InHand_GameObjects = new List<GameObject>();
    [SerializeField] internal List<GameObject> cards_NotInHand_GameObjects = new List<GameObject>();
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
        for (int i = 0; i < PlayerData.go_spellsIndeck.Length; ++i)
        {
            print(PlayerData.go_spellsIndeck[i].name);
            GameObject obj = Instantiate(PlayerData.go_spellsIndeck[i], deckPosition_Transform.transform);
            obj.SetActive(false);
            cards_NotInHand_GameObjects.Add(obj);
        }

    }
    //Place 5 random cards in Hand
    private void GetFirstHand()
    {
        for (int i = 0; i < handPositions_Transform.Count; ++i)
        {
            int randomIndex = Random.Range(0, cards_NotInHand_GameObjects.Count);

            // Add Card to hand
            cards_InHand_GameObjects.Add(cards_NotInHand_GameObjects[randomIndex]);
            // Remove Card from Deck
            cards_NotInHand_GameObjects.RemoveAt(randomIndex);

            cards_InHand_GameObjects[i].SetActive(true);
            cards_InHand_GameObjects[i].gameObject.transform.position = handPositions_Transform[i].position;
            cards_InHand_GameObjects[i].transform.GetComponent<SpellCard_Manager>().v3_currentPosition = handPositions_Transform[i].position;
        }
    }
    //USED CARD 
    // Disable the used card and move it back to the deck
    public void GetSpellBackToDeck(SpellCard_Manager usedCard, Vector3 previousPosition)
    {
        cards_InHand_GameObjects.Remove(usedCard.gameObject);
        cards_NotInHand_GameObjects.Add(usedCard.gameObject);
        usedCard.gameObject.SetActive(false);
        usedCard.transform.position = deckPosition_Transform.position;
        usedCard.v3_currentPosition = deckPosition_Transform.position;
        GetSpellToHand(previousPosition);
    }
    //NEW CARD
    //Get new card from Deck to Hand and place it   
    private void GetSpellToHand( Vector3 peviousPosition)
    {
        
        cards_NotInHand_GameObjects[0].SetActive(true);
        cards_NotInHand_GameObjects[0].GetComponent<SpellCard_Manager>().v3_currentPosition = peviousPosition;
        cards_InHand_GameObjects.Add(cards_NotInHand_GameObjects[0]);
        //cards_NotInHand_GameObjects[0].transform.position = peviousPosition;
        
        StartCoroutine(MoveFromDeckToHand(peviousPosition));
    }
    //Deck-->Hand movement
    private IEnumerator MoveFromDeckToHand(Vector3 peviousPosition)
    {
        float elapsedTime = 0;
        while (elapsedTime < 0.3f)
        {
            cards_NotInHand_GameObjects[0].transform.position = Vector3.Lerp(deckPosition_Transform.position, peviousPosition, elapsedTime / 0.3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cards_NotInHand_GameObjects[0].transform.position = peviousPosition;
        cards_NotInHand_GameObjects.RemoveAt(0);
    }


    #endregion
}
