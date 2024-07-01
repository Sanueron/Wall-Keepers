using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseSpellsEffects
{
    #region Variables
    private Vector3 v3_endPosition;
    private Vector3 v3_startPosition;
    private float elapsedTime = 0f;
    [SerializeField] private Vector3 v3_Projectail_Cast_Position;
    private bool b_startMovement;
    private Quaternion InitialRotation;
   [SerializeField] private bool b_finishAnimation;
    private float f_journeyLength;
    private float startTime;
    #endregion

    #region Unity Life Cycle

    private void Awake()
    {
        InitialRotation = transform.rotation;
      
    }
    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield&&b_startMovement)
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
        float f_distCovered = (Time.time - startTime) * 1.0f * f_speed*2.0f;
        float fractionOfJourney = f_distCovered / f_journeyLength;
        transform.position = Vector3.Lerp(v3_startPosition, v3_endPosition, fractionOfJourney);
        if (transform.position == v3_endPosition)
        {
            StartCoroutine(Explosion());
            b_startMovement = false;
        }
    }
    // When everything is finished, the card is disabled
    private IEnumerator Explosion()
    {
        effect_Animator.SetTrigger("Explode");
        //Wait until the Explosion animation has finished 
        while (!b_finishAnimation)
        {
            yield return null;
        }
        b_finishAnimation = false;
        ResetEffect();
    }
    /*If We want to Separate Projectail UP and Projectail Down Animations again 

     if (fractionOfJourney > 0.5f)
        {
            effect_Animator.SetTrigger("GoingDown");
        }
    private void DirectlyDown()
    {
        if (v3_endPosition.y <= 3.5f)
        {
            effect_Animator.SetBool("Close", true);
            effect_Animator.SetTrigger("DirectlyDown");

        }
    }
    */
    #endregion
    #region Animation Methods
    internal void StartMovement()
    {
        startTime = Time.time;
        effect_Animator.SetTrigger("Casted");
        b_startMovement = true;
    }
    internal void RestoreRotation()
    {
        transform.rotation = InitialRotation;
    }
    public void FinishAnimation()
    {
        b_finishAnimation = true;
    }
    internal void PlayExplosionClip()
    {
        //Sound
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
