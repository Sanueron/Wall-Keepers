using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Suction_StaticInstant : BaseSpellsEffects
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
        CheckIfLevelPaused();

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
        print("Finished?");
        effect_Animator.SetTrigger(i_EnterEnd);
    }
    internal void Disable()
    {
        ResetEffect();
    }


    #endregion

   
    #region Enemy Effect Methods
    internal void Suck()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.moveToTarget_transform = gameObject.transform;
        }   
    }

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            RemoveFromEnemiesList(collision.gameObject.GetComponent<Enemy>());

        }
    }
    #endregion

}
