using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lofelt.NiceVibrations.MMSwitch;

public class Enemy : MonoBehaviour
{
    #region Enemy Stats and Info
    [Header("Stats")]
    [SerializeField] ScripteableEnemies baseStats;
    internal int i_hp, i_damage, i_speed, i_startSpeed;
    internal float f_rangeOfAction;
    #endregion

    #region Enemy States
    internal enum EnemyState { Moving, TakingAction, ReceiveDamage, Defeated }
    internal EnemyState e_enemyState;
    internal bool b_targetDetected = false;
    #endregion

    #region Moving Variables
    internal Transform wallToTarget_transform;
    [SerializeField] internal Transform moveToTarget_transform;
    #endregion

    #region Action
    [Header("Action")]
    [SerializeField] BaseEnemyAction enemyAction;
    #endregion

    #region Unity Life Cycle
    private void Awake()
    {
        e_enemyState = EnemyState.Moving;
        GetStats();
       
    }
    private void Update()
    {
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                MoveToTarget();
                break;
        }
    }
    #endregion

    #region General Methods
    internal void SwitchState(EnemyState newState)
    {
        e_enemyState=newState;
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                // Play anim
                break;
            case EnemyState.TakingAction:
                // Play anim
                break;
            case EnemyState.ReceiveDamage:
                // Play anim
                SwitchState(EnemyState.Moving);
                break;
            case EnemyState.Defeated:
                // Play anim
                break;
        }
    }
    private void GetStats()
    {
        i_hp = baseStats.i_hp;
        i_damage = baseStats.i_dmg;
        i_speed = baseStats.i_speed;
        i_startSpeed = i_speed;
        f_rangeOfAction = baseStats.f_RageOfAction;
    }
    private void TakeAction()
    {
        enemyAction.TakeAction();
    }
    private void MoveToTarget()
    {
        var step = i_speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, moveToTarget_transform.position, step);
    }
    internal void ReceiveDamage(int i_damageReceived)
    {
        print("Hurt:-" + i_damageReceived + " Total: " + i_hp);
        i_hp -= i_damageReceived;
        if(i_hp < 0)
        {
            // Defeated
            SwitchState(EnemyState.Defeated);
        }
        else
        {
            // Receive Damage
            //SwitchState(EnemyState.ReceiveDamage);
        }
    }
    internal void SlowDown(int decreaseValue)
    {
        print("SlowDown"+i_speed);
        i_speed = i_speed/decreaseValue;

    }

    internal void RestoreSpeed()
    {
        i_speed = i_startSpeed;

    }
    #endregion
}
