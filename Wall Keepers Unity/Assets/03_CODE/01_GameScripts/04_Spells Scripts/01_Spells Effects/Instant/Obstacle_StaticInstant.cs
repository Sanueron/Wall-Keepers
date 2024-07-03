using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Obstacle_StaticInstant : BaseSpellsEffects
{
    private bool b_flinched, b_canFlinch;
    private WaitForSeconds flintchTimer_WFS = new WaitForSeconds(0.5f);
    #region InstantArea Methods
    //Receive target 
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
    }
    private void OnEnable()
    {
        SetAnimationBool();
        StartCoroutine(RockWallFlichTimer());
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
        if (b_flinched)
        {
            // Aplicamos la fricción
            Vector3 friccion = -enemyScript.rb_Enemy.velocity.normalized * 0.1f;
            enemyScript.rb_Enemy.AddForce(friccion, ForceMode2D.Impulse);

            if (enemyScript.rb_Enemy.velocity.y <= 0)
            {
                b_flinched = false;
            }
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
    // When enemies collide with the wall, they stop and begin attacking it.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            enemyScript = collision.gameObject.GetComponent<Enemy>();
            if (b_canFlinch)
            {
                Flinch(3.0f);
            }
        } 
    }

    private IEnumerator RockWallFlichTimer()
    {
        b_canFlinch = true;
        yield return flintchTimer_WFS;
        b_canFlinch = false;
    }
    public void Flinch(float force)
    {
        if (enemyScript.rb_Enemy != null)
        {
            b_flinched = true;
            enemyScript.rb_Enemy.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("No pilla el RB");
        }
    }
    internal void ReceiveDamage(int i_damageReceived)
    {
        print("damaged: "+i_damageReceived);
        f_resistance -= i_damageReceived;
        if (f_resistance < 0)
        {
            Disable();
        }
      
    }
}
