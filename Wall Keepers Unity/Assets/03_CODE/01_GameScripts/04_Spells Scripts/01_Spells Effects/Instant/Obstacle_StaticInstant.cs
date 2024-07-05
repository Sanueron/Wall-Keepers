using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Obstacle_StaticInstant : BaseSpellsEffects
{
    private bool b_flinched, b_canFlinch;
    private WaitForSeconds flintchTimer_WFS = new WaitForSeconds(0.5f);
    private float f_flinchForce = 3.5f;
    private float f_stopEnemyDelay = 0.3f;
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
    // When enemies collide with the wall, they stop and begin attacking it.
    public void Flinch()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.rb_Enemy.AddForce(transform.up * f_flinchForce, ForceMode2D.Impulse);
            StartCoroutine(StopEnemyAfterDelay(enemy, f_stopEnemyDelay));
        }
    }
    private IEnumerator StopEnemyAfterDelay(Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.rb_Enemy.velocity = Vector2.zero;
        enemy.rb_Enemy.angularVelocity = 0f;  // Si también quieres detener cualquier rotación
    }
    internal bool ReceiveDamage(int i_damageReceived)
    {
        f_resistance -= i_damageReceived;
        if (f_resistance < 0)
        {
            Disable();
            return false;
        }
        else
        {
              return true;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
    }
}
