using UnityEngine;

[CreateAssetMenu(fileName = "EnemieStats", menuName = "ScriptableEnemie/EnemieStats", order = 1)]
public class ScripteableEnemies : ScriptableObject
{
    [Header("Stats")]
    public int i_id;
    public int i_hp;
    public int i_dmg;
    public int i_speed;
    public float f_RageOfAction;
    public int i_manaEnergyReleased;

    [Header("SFXs")]
    public AudioClip deathClip;


    public enum Clan { None, Chaos, Newt, Earth, Shadow };
    [Header("Types")]
    public Clan e_Clan;

    public enum ElementalType { None, Earth, Fire, Water };
    public ElementalType e_ElementalType;
}
