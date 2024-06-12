using UnityEngine;

[CreateAssetMenu(fileName = "CardStats", menuName = "ScriptableObjects/CardStats", order = 1)]
public class ScripteableCard : ScriptableObject
{
    public int i_id;
    public int i_spellStarsTier;
    public int i_mana_cost;
    public float f_dmg;
    public float f_duration;  
    public float f_projectileSpeed;
    public float f_resistance;
    public Sprite _card_Sprite;
    public Sprite _onField_Sprite;// QUE ES MEJOR COJER EL SPRITE DE AQUI O EN SPELLCARD QUE EN FUNCION DEL TIPO DE CARTA COJA UNO U OTRO ?
    public AudioClip castClip;
    public AudioClip impactClip;
    public int i_starLevel = 1;
    [Header("Lake")]
    public float f_repeatingDmgCoolDown;//LakeLifeTime
    public enum castingType {None, Projectile, Instant, AllMap };
    public castingType e_CastingType;
    public enum effect { None, Impact, Area, ImpactArea };
    public effect e_Effect;
    public enum element { None,Fire, Earth, Water, Wind, Electric};
    public element e_Element;
    public enum element2 { None, Fire, Earth, Water, Wind, Electric };
    public element2 e_2Element;
    public string Description;
    [Header("Tonado")]
    //Para cuando metamos mas
    public float f_attackInterval;
    [Header("FusedCards")]
    public bool b_isMerged;
    public Sprite fusionSprite1;
    public Sprite fusionSprite2;
    public Sprite fusionSprite3;
    public Sprite fusionSprite4;  
    public Sprite fusionSprite5;
    public bool b_canAwake;
    public int i_awakenLevel = 1;


}
