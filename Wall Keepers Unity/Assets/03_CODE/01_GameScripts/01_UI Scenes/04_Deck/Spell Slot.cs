using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlot : BaseSlot
{
    public void BlockUseButtonIfDeckSlotsComplete()
    {
        button_addOrRemoveSpellToFromDeck.interactable = true;

        // Now check if Deck is complete and if so mark interactable as false
        if (PlayerData.b_isDeckFull)
        {
            button_addOrRemoveSpellToFromDeck.interactable = false;
        }
    }

}
