using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveCardManager : MonoBehaviour
{
    // Two functions: 1 for Level Up, 2 For Awakening lvl up

    [Header("Spell Level Up Animation")]
    [SerializeField] private Animator animator_lvlUPEffect;
    private int i_animatorIntVariable = Animator.StringToHash("ElementID");

    public void LevelUpSpell()
    {
        // Play Animation Effect

        // Play SFXs

        // Show Stats increase

        // Reduce Card Mana Energy

        // Reduce Number of Cards in Stock
    }
}
