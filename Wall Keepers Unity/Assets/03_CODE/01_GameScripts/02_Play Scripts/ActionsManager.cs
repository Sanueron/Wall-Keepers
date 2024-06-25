using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionsManager : MonoBehaviour
{
    #region Actions control Variables 
    [SerializeField] internal bool b_busy; // Check if any action on going, can't do other actions while busy is true
    internal bool b_showingCardInfo; // Used to know if Card info is been shown and then in Card Manager when card return to target position don't set busy back to fals
    [SerializeField]private bool b_draggingCard;
    private float f_timer = 0;
    private bool b_timerIsOn;
    public bool GetIsDragging()
    {
        return b_draggingCard;
    }
    #endregion
    #region Drag Variables
    private Vector3 v3_cardInitialPosition;
    private Vector2 v2_screenPosition;
    private Vector3 v3_worldPosition;
    [SerializeField] Vector3 v3_projectailCast_Area;
    [SerializeField] private SpellCard_Manager selectedSpell;
    private int i_sortingOrder_WhenDragging = 90, i_defaultSortingOrder = 70;
    [SerializeField]LayerMask spellCards_LayerMask;
    [SerializeField] List<GameObject> effectList_GameObject = new List<GameObject>();
    #endregion
    #region Unity Life Cycle
    private void Update()
    {
       
      
        if (b_draggingCard) // if action is card dragging 
        {
            // Check if player stops draggind and tries to drop card
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                DropCard();
                return;
            }
        }

        if (b_busy && !b_draggingCard) // If action ongoing, and action is not card dragging
        {
            return; //Stop
        }

        if (Input.touchCount > 0) // input for mobile
        {
          
            v2_screenPosition = Input.GetTouch(0).position;
          

            // If no other action ongoing or dragging any card, check if player trying to drag a card.
            GetWorldPositionBasedOnScreenTouchPosition();
            CheckIfTryingToDragCard();
        }
        else if (Input.GetMouseButton(0)) // Input for computers
        {

            Vector3 mousePosition = Input.mousePosition;
            v2_screenPosition = new Vector2(mousePosition.x, mousePosition.y);
            // If no other action ongoing or dragging any card, check if player trying to drag a card.
            GetWorldPositionBasedOnScreenTouchPosition();
            CheckIfTryingToDragCard();
        }
    }
    #endregion
    #region Drag Cards Methods
    private void InitCardDrag()
    {
        b_busy = true;
        b_draggingCard = true;
        // Place card above the others
        SetCardSortingOrder(i_sortingOrder_WhenDragging, selectedSpell);

    }
    //CardMovement
    private void CardDragging()
    {
        selectedSpell.transform.position = new Vector2(v3_worldPosition.x, v3_worldPosition.y);
    }
    private void DropCard()
    {
        b_busy = false;
        b_draggingCard = false;
        float f_timeLimitForShowingCardInfoPanel = .2f;
        if (f_timer < f_timeLimitForShowingCardInfoPanel)
        {
            // Show card info panel 
            Debug.Log("Show card info panel");
            //ShowCardInfoPanel(selectedSpell);
        }
        b_timerIsOn = false;
        f_timer = 0;
        print("CardDroped");
        if (selectedSpell.transform.position.y>= -6.2f)//  Battlefield collider 
        {
            // Check if enough Mana to cast
            if (ManaManager.instance.GetCurrentMana()> selectedSpell.GetCardData().i_mana_cost) // selectedSpell.MadnessEnergyCost()
            {
                //Reduce Mana 
                ManaManager.instance.ReduceCurrentMana(selectedSpell.GetCardData().i_mana_cost);
                //Active Effect
                //selectedSpell.ActiveEffect(selectedSpell.v3_cardInitialPosition, selectedSpell.transform.position);
                print("Aquí tiramos effecto de hechizo");
                selectedSpell.ActivateSpellEffect();
                //Reset card 
                PlayMode_DeckManager.instance.GetSpellBackToDeck(selectedSpell, selectedSpell.v3_cardInitialPosition);
               
            }
            else
            {
                Debug.Log("Return to hand - Show message - Not enough Mana");
                StartCoroutine(ReturnToHandPosition(selectedSpell));
            }

        }
        else
        {
            // Return card to initial position
            //selectedCard.transform.position = cardInitialPosition;
            Debug.Log("Return to hand - Show message - Can't cast in here");
            StartCoroutine(ReturnToHandPosition(selectedSpell));
        }

        //selectedSpell.GetCardVisuals().SetCardSortingOrder(i_defaultSortingOrder);


        selectedSpell = null;
    }

    //A method to return the card to its slot
    private IEnumerator ReturnToHandPosition( SpellCard_Manager selectedSpell)
    {

        print("dale");
        Vector3 startPosition = selectedSpell.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < 0.3f)
        {
            selectedSpell.transform.position = Vector3.Lerp(startPosition, selectedSpell.v3_cardInitialPosition, elapsedTime / 0.3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.position = selectedSpell.v3_cardInitialPosition;
        SetCardSortingOrder(i_defaultSortingOrder, selectedSpell);
      
    }
    private void SetCardSortingOrder(int sortingOrder, SpellCard_Manager selectedSpell)
    {
        selectedSpell.GetComponent<Renderer>().sortingOrder = sortingOrder;

    }
    //Get the SpriteRenderer from selectedSpell
    #endregion
    #region Additional Methods 
    private void GetWorldPositionBasedOnScreenTouchPosition()
    {

        v3_worldPosition = Camera.main.ScreenToWorldPoint(v2_screenPosition);
    }
    private void CheckIfTryingToDragCard()
    {
        if (b_draggingCard) // Already dragging card
        {
            CardDragging();
        }
        else // Check if trying to drag card
        {
            RaycastHit2D hit = Physics2D.Raycast(v3_worldPosition, Vector2.zero, 1000f,spellCards_LayerMask); // draw raycast

            if (hit.collider != null) // Raycast hits collider
            {
                 
                SpellCard_Manager spellCardInHand = hit.transform.GetComponent<SpellCard_Manager>();
                if (spellCardInHand != null) // If collider hitted is Spell Card
                {
                    // Start timer to see of trying to see card info only. Timer is checked in Drop method to know touch was fast.
                    b_timerIsOn = true;

                    // Check to indicate if Card can be dragged
                    selectedSpell = spellCardInHand;

                    InitCardDrag();
                }
            }
        }
    }

    private void ShowCardInfoPanel(SpellCard_Manager selectedCard)
    {
        b_busy = true; // Prevent player from dragging cards or doing other actions
        b_showingCardInfo = true;
        //MenuManager.instance.ShowSelectedCardInfoMenu(selectedCard);
    }
    #endregion

}
