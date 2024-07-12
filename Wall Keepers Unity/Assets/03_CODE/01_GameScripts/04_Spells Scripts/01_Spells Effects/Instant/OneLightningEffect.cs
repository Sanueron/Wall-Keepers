using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLightningEffect : BaseSpellsEffects
{
    #region Unity Life Cycle
   
    private void FixedUpdate()
    {
        CheckIfLevelPaused();
    }
    #endregion  
    #region InstantArea Methods
    //Receive target 
  
    //Wait until spell effect finish 
     private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = target;

    }
    #endregion
    
    #region Collisions
    // When the enemy collides with this effect (when the collider is enabled), it calls the enemy's ReceiveDamage function.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
    }

    public void SetDmg(float dmg)
    {
        f_damage= dmg;  

    }
    #endregion
    internal void ResetLightning()
    {
        enemiesScripts_List.Clear();
        b_spellEffectActiveInBattlefield = false;
        gameObject.SetActive(false);
    }
}
