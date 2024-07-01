using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown_StaticProjectail : BaseSpellsEffects
{
    #region Variables
    private Vector3 v3_endPosition;
    private Vector3 v3_startPosition;
    private float elapsedTime = 0f;
    [SerializeField] private Vector3 v3_Projectail_Cast_Position;
    private float f_journeyLength;
    private float startTime;
    private bool b_startMovement;
    private bool b_finishAnimation;
    private Quaternion InitialRotation;
    private int i_start_spriteSortingOrder;
    private SpriteRenderer spell_SpriteRenderer;
    private Enemy enemyScript;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        GetStartVariables();
    }

    private void GetStartVariables()
    {
        spell_SpriteRenderer = GetComponent<SpriteRenderer>();
        i_start_spriteSortingOrder = spell_SpriteRenderer.sortingOrder;
    }

    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield && b_startMovement)
        {
            ProjectailMovement();
        }
    }
    #endregion

    #region Base Methods
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        // Set new behaviour for Projectiles
        CastBall(starPosition, target);
    }
    #endregion

    #region Projectile Methods
    // Method to save the target position
    public void CastBall(Vector3 starPosition, Vector3 target)
    {
        transform.parent = null;
        transform.position = starPosition;
        v3_startPosition = starPosition;
        print(starPosition);
        v3_endPosition = target;
        f_journeyLength = Vector3.Distance(v3_startPosition, v3_endPosition);
    }
    private void ProjectailMovement()
    {
        //ROTATE TOWARDS TARGET
        Vector3 directionToTarget = v3_endPosition - transform.position;
        float angulo = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo - 90));
        //INTERPOLATION
        //We need to know the proportion of the journey that has been completed, using fractionOfJourney to make a good interpolation between the start and end positions.
        //Without this, the projectile takes the same time to reach all positions.
        float f_distCovered = (Time.time - startTime) * 1.0f * f_speed;
        float fractionOfJourney = f_distCovered / f_journeyLength;
        transform.position = Vector3.Lerp(v3_startPosition, v3_endPosition, fractionOfJourney);
        if (transform.position == v3_endPosition)
        {
            Explosion();
            b_startMovement = false;
        }
    }
    // When everything is finished, the card is disabled
    private void Explosion()
    {
        effect_Animator.SetTrigger("Explode");
        
        
    }
    private void StartLake()
    {
        StartCoroutine(Lake());
        effect_Animator.SetTrigger("Lake");
    }
    private IEnumerator Lake()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        ResetEffect();
    }

    internal void StartMovement()
    {
        startTime = Time.time;
        effect_Animator.SetTrigger("Casted");
        b_startMovement = true;
    }
   

    //Metodos que se repiten con el projectile, por lo tanto creo que esta clase deberia de heredar directamente de Projectile 
    public void FinishAnimation()
    {
        b_finishAnimation = true;
    }
    internal void PlayExplosionClip()
    {
        //Sound
    }
    internal void RestoreRotation()
    {
        transform.rotation = InitialRotation;
    }
    internal void Exploded()
    {
        spell_SpriteRenderer.sortingOrder = spell_SpriteRenderer.sortingOrder - 1;
    }
    private void ResetVariables()
    {
        spell_SpriteRenderer.sortingOrder = i_start_spriteSortingOrder;
    }

    #endregion
    #region Collisions
    // When an enemy enters the area, repeatedly hurt the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            enemyScript = collision.gameObject.GetComponent<Enemy>();
            SlowDown();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       EnemyOut();
    }
    private void SlowDown()
    {
        enemyScript.SlowDown(i_slowDownValue);
    }
    private void EnemyOut()
    {
        enemyScript.RestoreSpeed();
    }
    
    #endregion
}
