using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using static Enemy;

public class SuctionDmg_StaticInstant : BaseSpellsEffects
{

    #region Unity Life Cycle
    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield)
        {
            StartCoroutine(CastInBattlefield());
        }
    }
    private void OnDisable()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.RestoreTarget();
            enemy.OutOfAreaApplyingForceToEnemy();
        }
        // Empty List when effect is over
        enemiesScripts_List.Clear();
    }
    #endregion

    #region InstantArea Methods
    //Receive target 
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
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
        effect_Animator.SetTrigger(i_EnterEnd);
    }
  
    #endregion

    #region Suck Methods
    internal void Suck()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.moveToTarget_transform = gameObject.transform;
            enemy.b_beingAttracktedBySpell = true;
        }
    }
 
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().RestoreTarget();
            collision.gameObject.GetComponent<Enemy>().OutOfAreaApplyingForceToEnemy();
            RemoveFromEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
    }
    
    #endregion
}
