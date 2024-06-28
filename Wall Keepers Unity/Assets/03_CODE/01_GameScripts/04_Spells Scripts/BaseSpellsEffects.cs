using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellsEffects : MonoBehaviour
{
    #region Local Stats variables
    [SerializeField] internal Animator effect_Animator;
    internal bool b_spellEffectActiveInBattlefield = false;
    protected float f_damage, f_resistance, f_speed, f_duration;
    protected bool b_mergedCard;
    internal Transform spellCardParent;
    #endregion
    #region Local Common Methods
    internal void SetSpellStatsInSpellEffect(Transform tParent, float dmg, float res, float speed, float dur)
    {
        spellCardParent = tParent;
        f_damage = dmg;
        f_resistance = res;
        f_speed = speed;
        f_duration = dur;
     
    }
    public virtual void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        // Activate every single effect, override in each spell
    }
    internal void ResetEffect()
    {
        b_spellEffectActiveInBattlefield = false;
        transform.parent = spellCardParent;
        transform.position = spellCardParent.position;
        gameObject.SetActive(false);
    }
    //Coroutine to wait until the spell establisehed duration

    internal void PlayCastClip()
    {

        //Sound
    }
    #endregion
}
