using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using static Enemy;

public class SuctionDmg_StaticInstant : BaseSpellsEffects
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
    private void SetAnimationBool()
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
    internal IEnumerator CastInBattlefield()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        FinishLoop();
    }
 
    internal void FinishLoop()
    {
        effect_Animator.SetTrigger("FinishLoop");
    }
    internal void Disable()
    {
        ResetEffect();
    }
    internal void Suck()
    {
        enemyScript.moveToTarget_transform = gameObject.transform;
    }

    private void EnemyOut()//When the Tornado finish and the Enemy is oustide this collider, the enemy recovers his Movement
    {
        enemyScript.RestoreTarget();
    }


    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            enemyScript = collision.gameObject.GetComponent<Enemy>();
            Suck();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            EnemyOut();

        }
    }
}
