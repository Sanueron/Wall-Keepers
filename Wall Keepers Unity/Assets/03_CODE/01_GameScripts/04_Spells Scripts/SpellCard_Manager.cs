using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpellCard_Manager : MonoBehaviour
{
    public Vector3 v3_cardInitialPosition;
    [SerializeField] private ScripteableCard spellBaseData;
    [SerializeField] private GameObject _spellEffect_GameObject;

    #region Spell Stats variables
    private int i_spellLevel, i_spellAwakeningLvl;
    private float f_damage, f_resistance, f_speed, f_duration;
    #endregion

    #region Spell Effect variables
    [Header("Spell effect")]
    [SerializeField] internal BaseSpellsEffects spellEffectPrefab;
    [SerializeField] internal List<BaseSpellsEffects> listOf_spellEffects = new List<BaseSpellsEffects>();
    [SerializeField] internal int i_numberOfSpellEffectsInStock;
    #endregion

    private void Start()
    {
        InitializedCard();
        SetSpellInitialStats();
        InitializeSpellEffectsPrefabs();
    }
    private void InitializedCard()
    {
        v3_cardInitialPosition = transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = spellBaseData._card_Sprite;
    }

    #region Spell Base Data and Stats
    public ScripteableCard GetCardData()
    {
        return spellBaseData;   
    }
    private void SetSpellInitialStats()
    {
        // Get Spell Level and Awakening Level
        int spellID = spellBaseData.i_id;
        i_spellLevel = PlayerData.i_spell_Card_Level[spellID];
        i_spellAwakeningLvl = PlayerData.i_awakening_Card_Level[spellID];

        #region Get spell stats updated to level and awakening level
        // Damage
        f_damage = spellBaseData.f_dmg +
                SpellsData.f_damageIncreaseByLevel * i_spellLevel +
                SpellsData.f_damageIncreaseByAwakening * i_spellAwakeningLvl;
        // Resistance
        f_resistance = spellBaseData.f_resistance +
                SpellsData.f_resistanceIncreaseByLevel * i_spellLevel +
                SpellsData.f_resistanceIncreaseByAwakening * i_spellAwakeningLvl;
        // Speed
        f_speed = spellBaseData.f_projectileSpeed +
                SpellsData.f_speedIncreaseByLevel * i_spellLevel +
                SpellsData.f_speedIncreaseByAwakening * i_spellAwakeningLvl; // if 0 is instant casting
        // Duration
        f_duration = spellBaseData.f_duration +
                SpellsData.f_durationIncreaseByLevel * i_spellLevel +
                SpellsData.f_damageIncreaseByAwakening * i_spellAwakeningLvl;
        #endregion
    }
    #endregion

    #region Spell Effects
    private void InitializeSpellEffectsPrefabs()
    {
        
        for (int i = 0; i < i_numberOfSpellEffectsInStock; i++)
        {
            BaseSpellsEffects spellEffect = Instantiate(spellEffectPrefab, transform);
            listOf_spellEffects.Add(spellEffect);
        }

        // initialize Stats
        foreach(BaseSpellsEffects spellsEffects in listOf_spellEffects)
        {
            // Pass to spell effect spells stats updated with level and awakening level
            spellsEffects.SetSpellStatsInSpellEffect(transform, f_damage, f_resistance, f_speed, f_duration);
            spellsEffects.gameObject.SetActive(false);
        }
    }
    internal void ActivateSpellEffect()
    {
        // Take first spell effect in list which is not active already
        for (int i = 0; i < listOf_spellEffects.Count; i++)
        {
            // Check if spell effect is not still active in battlefield
            if (!listOf_spellEffects[i].b_spellEffectActiveInBattlefield)
            {
                // Activate effect
                listOf_spellEffects[i].gameObject.SetActive(true);
                // Activate Spell effect
                listOf_spellEffects[i].ActivateEffect(v3_cardInitialPosition, transform.position);
                // Set spell state to active
                listOf_spellEffects[i].b_spellEffectActiveInBattlefield = true;
                // Stop loop to activate effect once
                break;
            }
        }
    }
    #endregion

    public void ActiveEffect(Vector3 starPosition, Vector3 target)
    {
        // Esto sólo funcionará para los proyectiles, ¿Que pasa para el resto de las cartas? No es modular
        ProjectileEffect effect_script = _spellEffect_GameObject.GetComponent<ProjectileEffect>(); 

        effect_script.CastBall(starPosition, target);
        _spellEffect_GameObject.SetActive(true);
    }

    public void DisableCard()
    {
        gameObject.SetActive(false);    
    }

}
