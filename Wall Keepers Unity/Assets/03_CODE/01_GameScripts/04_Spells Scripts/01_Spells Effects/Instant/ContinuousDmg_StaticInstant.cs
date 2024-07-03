using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDmg_StaticInstant : BaseSpellsEffects
{
  
    #region InstantArea Methods
    //Receive target 
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
    }

    private void OnEnable()
    {
        SetAnimationBool();
    }
    private void SetAnimationBool()//This method is Only for walls 
    {
     effect_Animator.SetBool("Wall", true);
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
    internal void Disable()
    {
        ResetEffect();
    }

    internal IEnumerator CastInBattlefield()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        ResetEffect();
    }
 
    public void ContinuousDmg()
    {
        if(enemyScript != null)
        {
            enemyScript.ReceiveDamage((int)f_damage);
        }
    }
    #endregion
    #region Collisions
    // When an enemy enters the area, repeatedly hurt the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            enemyScript = collision.gameObject.GetComponent<Enemy>();
            InvokeRepeating("ContinuousDmg", .0f,f_repeatRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("ContinuousDmg");
    }
    #endregion
}
