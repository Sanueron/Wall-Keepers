using UnityEngine;

[CreateAssetMenu(fileName = "EnemieStats", menuName = "ScriptableEnemie/EnemieStats", order = 1)]
public class ScripteableEnemies : ScriptableObject
{

    public float f_speed;
    public float f_stepSize;
    public float f_life;
    public float f_dmg;
    public float f_lateralSpeed;
    public float f_RageAttack;
    public AudioClip deathClip;
    public enum ElementalType { None, Earth, Fire, Water };
    public ElementalType e_ElementalType;
    public enum Clan { None, Chaos, Newt, Earth, Shadow };
    public Clan e_Clan;

    public enum Class { None, Basic, Crazy, Bulky};
    public Class e_Class;
    //collider??
}
