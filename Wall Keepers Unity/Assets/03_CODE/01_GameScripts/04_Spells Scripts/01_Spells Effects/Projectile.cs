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
    private bool b_finishAnimation;

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
        print("InitialPos" + transform.position);
        print("EndPos" + v3_endPosition);
    }
    private void ProjectailMovement()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;
        // Calculate the percentage of elapsed time
        float t = elapsedTime / f_speed;
        if (v3_endPosition.y <= 3.5f)
        {
            effect_Animator.SetBool("Close", true);
            effect_Animator.SetTrigger("DirectlyDown");

        }
        transform.position = Vector3.Lerp(v3_startPosition, v3_endPosition, t);
        if (transform.position == v3_endPosition)
        {
            elapsedTime = 0f;   
            print("Impacto");
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
            b_finishAnimation = false;

            yield return null;

        }
      
        ResetEffect();
    }

    #endregion
    #region Animation Methods
    internal void StartMovement()
    {
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
}
