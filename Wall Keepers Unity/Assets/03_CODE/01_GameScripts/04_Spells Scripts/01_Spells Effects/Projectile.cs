using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseSpellsEffects
{
    #region Variables
    private Vector3 v3_endPosition;
    private Vector3 v3_startPosition;
    private Animator _spell_Animator;
    private float journeyLength;
    private bool b_finishAnimation;
    private WaitForSeconds lakeLifeTime_WFS;
    private float elapsedTime = 0f;
    #endregion

    #region Unity Life Cycle
    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield)
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
        v3_startPosition = starPosition;
        v3_endPosition = target;

    }
    private void ProjectailMovement()
    {

        // Increment the elapsed time
        elapsedTime += Time.deltaTime;
        // Calculate the percentage of elapsed time
        float t = elapsedTime / f_speed;

        transform.position = Vector3.Lerp(v3_startPosition, v3_endPosition, t);
        if (transform.position.y >= v3_endPosition.y)
        {
            Explosion();
        }
    }
    // When everything is finished, the card is disabled
    private void Explosion()
    {
        //transform.parent.GetComponentInChildren<SpellCard_Manager>().DisableCard(); // No entiendo esto
        print("Explota proyectil");
    }
    #endregion
}
