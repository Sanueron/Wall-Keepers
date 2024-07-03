using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Lofelt.NiceVibrations.MMSwitch;

public class Enemy : MonoBehaviour
{
    #region Enemy Stats and Info
    [Header("Stats")]
    [SerializeField] ScripteableEnemies baseStats;
    internal int i_hp, i_damage, i_speed, i_startSpeed;
    internal float f_rangeOfAction;
    internal Rigidbody2D rb_Enemy;
    [SerializeField]private Animator enemy_animator;
    private bool b_CannotWalk;
    WallManager wallScript;
    Obstacle_StaticInstant targetObstacle; // cuando lo detecte el raycast guardarlo aqui 
   
    #endregion

    #region Enemy States
    internal enum EnemyState { Moving, TakingAction, ReceiveDamage, Defeated,Attack, Stop }
    internal EnemyState e_enemyState;
    internal bool b_targetDetected = false;
    #endregion

    #region Moving Variables
    internal Transform wallToTarget_transform;
    [SerializeField] internal Transform moveToTarget_transform;
    private Transform wallTarget_transform;
    internal float f_speedDecreasedValue=1.0f;
    #endregion


    #region Action
    [Header("Action")]
    [SerializeField] BaseEnemyAction enemyAction;



    #endregion
    #region Animations
    [Header("Animations")]
    private int i_WalkHash, i_AttackHash, i_HitHash, i_DefeatedHash;
    #endregion




    #region Unity Life Cycle
    private void Awake()
    {
        e_enemyState = EnemyState.Moving;
        GetStats();
        InitializeAnimations();
       
    }

     
    private void InitializeAnimations()
    {
        i_WalkHash = Animator.StringToHash("Walking");
        i_AttackHash = Animator.StringToHash("Attack");
        i_HitHash = Animator.StringToHash("ReceiveHit");
        i_DefeatedHash = Animator.StringToHash("Defeated");
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

    private void FixedUpdate()
    {
        DetectObstacle();
    }


    private void DetectObstacle()
    {
        int wallLayer = 1 << 6;   // Wall Layer
        int obstacleLayer = 1 << 7; // Obstacle Layer
        int layerMask = wallLayer | obstacleLayer; // Layer Combination
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, -transform.up, f_rangeOfAction, layerMask);
        Debug.DrawLine(transform.position, transform.position - transform.up * f_rangeOfAction, Color.red);

        if(raycastHit2D.collider != null)
        {
            print(raycastHit2D.collider.gameObject.name);
            print("ObstacleDetected");
            targetObstacle= raycastHit2D.collider.gameObject.GetComponent<Obstacle_StaticInstant>();
            SwitchState(EnemyState.TakingAction);
        }
    }
    #region General Methods
    internal void SwitchState(EnemyState newState)
    {
        e_enemyState=newState;
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                enemy_animator.SetTrigger(i_WalkHash);
                break;
            case EnemyState.TakingAction:
                enemy_animator.SetTrigger(i_AttackHash);
                // Play anim
                break;
            case EnemyState.ReceiveDamage:
                enemy_animator.SetTrigger(i_HitHash);
                break;
            case EnemyState.Defeated:
                // Play anim
                break;
      
        }
    }
    //Metodo para checkear 
    private void GetStats()
    {
        i_hp = baseStats.i_hp;
        i_damage = baseStats.i_dmg;
        i_speed = baseStats.i_speed;
        f_rangeOfAction = baseStats.f_RageOfAction;

        //Start variables
        i_startSpeed = i_speed;
        wallTarget_transform = moveToTarget_transform;
        rb_Enemy = gameObject.GetComponent<Rigidbody2D>();  
    }
    internal void TakeAction()// llamar a esto desde el evento 
    {
        print("Calling");
       //enemyAction.TakeAction(wallScript, targetObstacle);
    }
    private void MoveToTarget()
    {
        var step = i_speed* f_speedDecreasedValue * Time.deltaTime; // calculate distance to move
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
            SwitchState(EnemyState.ReceiveDamage);
            //Comprobar si tiene que atacar o moverse 
        }
    }
    internal void CheckState()
    {
        if (b_CannotWalk)//If there are a obstacle or Wall, Attack 
        {
            SwitchState(EnemyState.Attack);
        }
        else//If not, Walk
        {
            SwitchState(EnemyState.Moving);
        }
    }
    internal void RestoreSpeed()
    {
        print("SpeedRestored");
        f_speedDecreasedValue = 1.0f;
    }
    internal void RestoreTarget()
    {
        moveToTarget_transform = wallTarget_transform;
    }
    #endregion
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.gameObject.layer == 11)
        {
            b_CannotWalk = true;
            SwitchState(EnemyState.TakingAction);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            b_CannotWalk = false;
            SwitchState(EnemyState.Moving);
        }
    }
}
