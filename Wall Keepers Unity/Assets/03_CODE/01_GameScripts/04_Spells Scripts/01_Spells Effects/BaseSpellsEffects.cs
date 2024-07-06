using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
    #region Animations
    [Header("Animations")]
    protected int i_EnterLoop = Animator.StringToHash("Enter Loop"),
        i_EnterEnd = Animator.StringToHash("Enter End"),
        i_EnterExtraLoop = Animator.StringToHash("Enter Extra Loop"),
        i_EnterExtraEnd = Animator.StringToHash("Enter Extra End");
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


    #endregion
    #region PlaySounds
    internal void PlayCastClip()
    {

        //Sound
    }
    internal void PlayExplosionClip()
    {
        //Sound
    }
    #endregion

    #region Enemies List
    protected void AddToEnemiesList(Enemy enemy)
    {
        enemiesScripts_List.Add(enemy);
    }

    protected void RemoveFromEnemiesList(Enemy enemy)
    {

        enemiesScripts_List.Remove(enemy);
    }
    #endregion


    protected void StartEndAnimation()
    {
        effect_Animator.SetTrigger(i_EnterEnd);
    }

    protected void StartLoopAnimation()
    {
        effect_Animator.SetTrigger(i_EnterLoop);
    }

    internal void RestoreEnemyTarget()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.RestoreTarget();
        }
    }

    internal void HurtEnemies()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.ReceiveDamage((int)f_damage);
        }
    }


}
