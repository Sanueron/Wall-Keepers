using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellsEffects : MonoBehaviour
{
    #region Local Stats variables
    private bool b_spellEffectActiveInBattlefield = false;
    public bool GetSpellEffectActiveState() { return b_spellEffectActiveInBattlefield; }
    private float f_damage, f_resistance, f_speed, f_duration;
    #endregion

    #region Local Common Methods
    internal void SetSpellStatsInSpellEffect(float dmg, float res, float speed, float dur)
    {
        f_damage = dmg;
        f_resistance = res;
        f_speed = speed;
        f_duration = dur;
    }
    public virtual void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        // Activate every single effect, override in each spell
    }
    #endregion
}
