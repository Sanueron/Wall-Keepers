using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlusArea : BaseSpellsEffects
{
    #region Variables
    private Vector3 v3_endPosition;
    private Vector3 v3_startPosition;
    private float elapsedTime = 0f;
    [SerializeField] private Vector3 v3_Projectail_Cast_Position;
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

        transform.position = Vector3.Lerp(v3_startPosition, v3_endPosition, t);
        if (transform.position == v3_endPosition)
        {
           
            //Start coroutine to waint until the spell effect finish
            StartCoroutine(CastInBattlefield());
        }
    }

    internal IEnumerator CastInBattlefield()
    {
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
        elapsedTime = 0;
        ResetEffect();
    }


    #endregion
}
