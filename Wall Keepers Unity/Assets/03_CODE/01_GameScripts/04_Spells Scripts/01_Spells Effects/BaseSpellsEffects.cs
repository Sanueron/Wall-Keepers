using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpellsEffects : MonoBehaviour
{
    #region Local Stats variables
    [SerializeField] internal Animator effect_Animator;
    internal bool b_spellEffectActiveInBattlefield = false;
    protected float f_damage, f_resistance, f_speed, f_duration, f_repeatRate, f_slowDownValue;
    protected bool b_mergedCard;
    internal Transform spellCardParent;
   [SerializeField] internal List <Enemy> enemiesScripts_List = new List<Enemy>();
    protected int i_EnemyLayer = 10;
    #endregion
    #region Local Common Methods
    internal void SetSpellStatsInSpellEffect(Transform tParent, float dmg, float res, float speed, float dur, float repeatRate, float slowDownValue)
    {
        spellCardParent = tParent;
        f_damage = dmg;
        f_resistance = res;
        f_speed = speed;
        f_duration = dur;
        f_repeatRate = repeatRate;
        f_slowDownValue = slowDownValue;


    }
    public virtual void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        // Activate every single effect, override in each spell
    }
    internal void ResetEffect()
    {
        enemiesScripts_List.Clear();
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

    protected void AddToEnemiesList(Enemy enemy)
    {
        enemiesScripts_List.Add(enemy);
    }

    protected void RemoveFromEnemiesList(Enemy enemy)
    {

        enemiesScripts_List.Remove(enemy);
    }

}
