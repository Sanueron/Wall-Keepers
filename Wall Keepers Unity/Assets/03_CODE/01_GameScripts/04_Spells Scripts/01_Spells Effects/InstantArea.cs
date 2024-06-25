using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantArea : BaseSpellsEffects
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
    private void FixedUpdate()
    {
        if (b_spellEffectActiveInBattlefield)
        {
            StartCoroutine(CastInBattlefield());
        }
    }


    #endregion
}
