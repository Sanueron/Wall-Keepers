using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Events
    public static event EventHandler onEnemyDefeated;
    #endregion

    #region Enemy Stats and Info
    [Header("Stats")]
    [SerializeField] ScripteableEnemies baseStats;
    internal int i_hp, i_damage, i_speed, i_startSpeed;
    internal float f_rangeOfAction;
    internal Rigidbody2D rb_Enemy;
    [SerializeField] private Animator enemy_animator;
    private bool b_CannotWalk;
    WallManager wallScript;
    Obstacle_StaticInstant targetObstacle; // cuando lo detecte el raycast guardarlo aqui 
    GameObject go_obstacle;
    GameObject go_wall;
    #endregion

    #region Enemy States
    internal enum EnemyState { Moving, TakingAction, ReceiveDamage, Defeated, Attack, Stop }
    [SerializeField] internal EnemyState e_enemyState;
    internal bool b_targetDetected = false;
    internal bool b_moveToTarget, b_receivingDamage, b_beingAttracktedBySpell;
    #endregion

    #region Moving Variables
    internal Vector3 targetToMove;
    internal Transform wallToTarget_transform;
    [SerializeField] internal Transform moveToTarget_transform;
    internal Transform wall_moveToTarget_transform;
    private Transform wallTarget_transform;
    internal float f_speedDecreasedValue = 1.0f;
    #endregion

    #region Action
    [Header("Action")]
    [SerializeField] BaseEnemyAction enemyAction;
    public int i_spawnNumber;


    #endregion

    #region Animations
    [Header("Animations")]
    private int i_WalkHash = Animator.StringToHash("Walking"),
        i_AttackHash = Animator.StringToHash("Attack"),
        i_HitHash = Animator.StringToHash("ReceiveHit"),
        i_DefeatedHash = Animator.StringToHash("Defeated");
    #endregion

    #region Unity Life Cycle
    private void OnEnable()
    {
        // Add enemy to list of enemies of this round
        RoundsManager.listOfEnemiesInRound.Add(this);

        GameObject go_wallMoveTo = GameObject.Find("Enemies Wall Move to Target");
        wall_moveToTarget_transform = go_wallMoveTo.transform;
        moveToTarget_transform = wall_moveToTarget_transform;
        RestoreTarget();
    }
    private void Start()
    {
        SwitchState(EnemyState.Moving);

        GetStats();

        // Set Initial Move to target pos and wall pos.
        GameObject go_wallMoveTo = GameObject.Find("Enemies Wall Move to Target");
        wall_moveToTarget_transform = go_wallMoveTo.transform;
        moveToTarget_transform = wall_moveToTarget_transform;
        RestoreTarget();

    }

    private void Update()
    {
        if (b_moveToTarget) // No obstacle, moves towards wall
        {
            MoveToTarget();
            return;
        }
        if (b_beingAttracktedBySpell)
        {
            MoveToTarget();
            return;
        }
    }
    private void FixedUpdate()
    {
        DetectObstacle();
    }
    #endregion

    #region General Methods
    private void DetectObstacle()
    {
        int wallLayer = 1 << 6;   // Wall Layer
        int obstacleLayer = 1 << 7; // Obstacle Layer
        int layerMask = wallLayer | obstacleLayer; // Layer Combination
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, -transform.up, f_rangeOfAction, layerMask);
        //Debug.DrawLine(transform.position, transform.position - transform.up * f_rangeOfAction, Color.red);

        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject.GetComponent<WallManager>() != null
                && !b_receivingDamage)
            {
                go_wall = raycastHit2D.collider.gameObject;
                SwitchState(EnemyState.TakingAction);
                return;
            }
            else if (raycastHit2D.collider.gameObject.GetComponent<Obstacle_StaticInstant>() != null
                && !b_receivingDamage)
            {
                go_obstacle = raycastHit2D.collider.gameObject;
              
                SwitchState(EnemyState.TakingAction);
                return;
            }
        }
        else // No obstacle detected
        {
            if (!b_receivingDamage)
            {
                // If enemy state is defeated dont move
                if(e_enemyState == EnemyState.Defeated) { return; }
                // If no obstacle detected and enemy not defeated move towards Wall
                SwitchState(EnemyState.Moving);
            }
        }
    }

    private IEnumerator WaitToTakeAction()
    {
        yield return new WaitForSeconds(0.3f);
        SwitchState(EnemyState.TakingAction);
    }
    internal void SwitchState(EnemyState newState)
    {
        e_enemyState = newState;
        SetAllStatesAnimsToFalse();
        switch (e_enemyState)
        {
            case EnemyState.Moving:
                go_obstacle = null;
                go_wall = null;
                enemy_animator.SetBool(i_WalkHash, true);
                b_moveToTarget = true; 
                break;
            case EnemyState.TakingAction:
                b_moveToTarget = false; // Stop moving
                enemy_animator.SetBool(i_AttackHash, true);
                // Play anim
                break;
            case EnemyState.ReceiveDamage:
                b_moveToTarget = false; // Stop moving
                b_receivingDamage = true; // Indicate enemy is receiving damage
                enemy_animator.SetBool(i_HitHash, true);
                break;
            case EnemyState.Defeated:
                print("Defeated");
                b_moveToTarget = false; // Stop moving
                // Play anim
                enemy_animator.SetTrigger(i_DefeatedHash);
                break;

        }
    }
    private void SetAllStatesAnimsToFalse()
    {
        enemy_animator.SetBool(i_WalkHash, false);
        enemy_animator.SetBool(i_AttackHash, false);
        enemy_animator.SetBool(i_HitHash, false);
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
        print("Takes Action");
    }
    private void MoveToTarget()
    {
        var step = i_speed * f_speedDecreasedValue * Time.deltaTime; // calculate distance to move
        if (b_beingAttracktedBySpell)
        {
            targetToMove = new Vector3(moveToTarget_transform.position.x, moveToTarget_transform.position.y, moveToTarget_transform.position.z);
        }
        else
        {
            targetToMove = new Vector3(transform.position.x, moveToTarget_transform.position.y, moveToTarget_transform.position.z);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetToMove, step);
    }
    internal void EndReceiveDamage()//Event that triggers when the damage animation ends so that the enemy knows what to do next.
    {
        b_receivingDamage = false;
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
        Vector3 wallMoveToTargetPostion = new Vector3(transform.position.x,
            wall_moveToTarget_transform.position.y, wall_moveToTarget_transform.position.z);
        //print("Enemy moves to " + wallMoveToTargetPostion);
        moveToTarget_transform.position = wallMoveToTargetPostion;
    }
    internal void OutOfAreaApplyingForceToEnemy()
    {
        b_beingAttracktedBySpell = false;
    }
    internal void ReceiveDamage(int i_damageReceived)
    {
        i_hp -= i_damageReceived;
        if (i_hp < 0)
        {
            SwitchState(EnemyState.Defeated);
            RoundsManager.listOfEnemiesInRound.Remove(this);
            onEnemyDefeated?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SwitchState(EnemyState.ReceiveDamage);
        }
    }
    #endregion

    #region Remove from Battlefield and Reset Methods
    internal void RemoveFromBattlefield()
    {
        gameObject.SetActive(false); // Disactivate enemy
    }
    internal void ResetEnemy()
    {
        // Set enemy stats and States to base
        //RestoreTarget();
        //SwitchState(EnemyState.Moving);
        GetStats();
    }
    #endregion

}