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
    GameObject go_obstacle;
    GameObject go_wall;
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
    private void Update()
    {
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                MoveToTarget();
                break;
        }
    }
    private void FixedUpdate()
    {
        DetectObstacle();
    }
    #endregion
    #region General Methods
    private void InitializeAnimations()
    {
        i_WalkHash = Animator.StringToHash("Walking");
        i_AttackHash = Animator.StringToHash("Attack");
        i_HitHash = Animator.StringToHash("ReceiveHit");
        i_DefeatedHash = Animator.StringToHash("Defeated");
    }
    private void DetectObstacle()
    {
        int wallLayer = 1 << 6;   // Wall Layer
        int obstacleLayer = 1 << 7; // Obstacle Layer
        int layerMask = wallLayer | obstacleLayer; // Layer Combination
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, -transform.up, f_rangeOfAction, layerMask);
        Debug.DrawLine(transform.position, transform.position - transform.up * f_rangeOfAction, Color.red);

        if (raycastHit2D.collider != null)
        {
            print(raycastHit2D.collider.gameObject.name);
            print("ObstacleDetected");
            if (raycastHit2D.collider.gameObject.GetComponent<WallManager>() != null)
            {
                go_wall = raycastHit2D.collider.gameObject;
            }
            else if (raycastHit2D.collider.gameObject.GetComponent<Obstacle_StaticInstant>() != null)
            {
                go_obstacle = raycastHit2D.collider.gameObject;
            }
            else
            {
                print("Obstacle NOT Registred");
            }

            //StartCoroutine(WaitToTakeAction());
            SwitchState(EnemyState.TakingAction);
        }
        else
        {
            SwitchState(EnemyState.Moving);
            
        }
    }

    private IEnumerator WaitToTakeAction()
    {
        yield return new WaitForSeconds(0.3f);
        SwitchState(EnemyState.TakingAction);
    }
    internal void SwitchState(EnemyState newState)
    {
        e_enemyState=newState;
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                go_obstacle = null;
                go_wall=null;
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

    internal void AttackEvent()// When the enemy attacks, we call this event which executes the enemyAction method.
    {
        enemyAction.TakeAction(go_obstacle, go_wall);
    }
    private void MoveToTarget()
    {
        var step = i_speed* f_speedDecreasedValue * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, moveToTarget_transform.position, step);
    }
    internal void CheckStateEvent()//Event that triggers when the damage animation ends so that the enemy knows what to do next.
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
    #endregion
    #region External Methods
    internal void RestoreSpeed()
    {
        print("SpeedRestored");
        f_speedDecreasedValue = 1.0f;
    }
    internal void RestoreTarget()
    {
        moveToTarget_transform = wallTarget_transform;
    }
    internal void ReceiveDamage(int i_damageReceived)
    {
        print("Hurt:-" + i_damageReceived + " Total: " + i_hp);
        i_hp -= i_damageReceived;
        if (i_hp < 0)
        {
            SwitchState(EnemyState.Defeated);
        }
        else
        {
            SwitchState(EnemyState.ReceiveDamage);
        }
    }
    #endregion
  
}
