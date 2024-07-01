using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamage : BaseSpellsEffects
{ 
    #region InstantArea Methods
    //Receive target 
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
    }
    //Wait until spell effect finish 
    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield)
        {
            StartCoroutine(CastInBattlefield());
        }
    }
    private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = target;

    }
    internal IEnumerator CastInBattlefield()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        ResetEffect();
    }
    internal void Disable()
    {
        ResetEffect();
    }




    #endregion
    #region Collisions
    // When the enemy collides with this effect (when the collider is enabled), it calls the enemy's ReceiveDamage function.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            print("Collision");
            collision.GetComponent<Enemy>().ReceiveDamage((int)f_damage);//We should use the same data type  ¿Float or Int?
        }
    }
    #endregion
}
