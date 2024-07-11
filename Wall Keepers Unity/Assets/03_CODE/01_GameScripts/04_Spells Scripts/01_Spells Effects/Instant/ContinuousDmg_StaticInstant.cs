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
    private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = target;

    }
    //Wait until spell effect finish 
    private void FixedUpdate()//1º Loop FireWall
    {
        CheckIfLevelPaused();
        if (b_spellEffectActiveInBattlefield)
        {
            StartCoroutine(CastInBattlefield());
        }
    }
 
    internal IEnumerator CastInBattlefield()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        effect_Animator.SetTrigger(i_EnterEnd);//When the is finised, start End animation
    }
 
    internal override void HurtEnemies()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.ReceiveDamage((int)f_damage);
        }
    }
    #endregion

    #region SFX Play Methods
    internal void StopLoopSfx()
    {
        aS_spellAudioSource.Stop();
    }
    #endregion

    #region Collisions
    // When an enemy enters the area, repeatedly hurt the enemy
    private void  OnTriggerEnter2D(Collider2D collision)
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
