using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
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


    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
            InvokeRepeating("ContinuousDmg", .0f, f_repeatRate);
            Suck();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            RemoveFromEnemiesList(collision.gameObject.GetComponent<Enemy>());
            EnemyOut();
            CancelInvoke("ContinuousDmg");
        }
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
    private void EnemyOut()//When the Tornado finish and the Enemy is oustide this collider, the enemy recovers his Movement
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.RestoreTarget();
        }
    }
    private void ContinuousDmg()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.ReceiveDamage((int)f_damage);
        }
    }
    #endregion



}
