using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellsEffects : MonoBehaviour
{
    #region Local Stats variables
    internal bool b_spellEffectActiveInBattlefield = false;
    protected float f_damage, f_resistance, f_speed, f_duration;
    #endregion

    #region 
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
    private void ResetEffect()
    {
        b_spellEffectActiveInBattlefield = false;
        transform.parent = spellCardParent;
        transform.position = new Vector3(0,0,0);
    }
    #endregion
}
