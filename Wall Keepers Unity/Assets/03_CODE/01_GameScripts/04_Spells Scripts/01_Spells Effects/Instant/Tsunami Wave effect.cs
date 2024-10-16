using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TsunamiWaveeffect : BaseSpellsEffects
{
    #region Variables
    private bool b_startMoving;
    [SerializeField] internal Vector3 targetToMove, startPosition, v3_correctSizeWithNoParent;
    #endregion

    #region Unity Life Cycle
    private void Update()
    {
        CheckIfLevelPaused();

        // Move wave
        if (b_startMoving)
        {
            if (!LevelsGnralManager.b_stopLevel)
            {
                f_counter += Time.deltaTime;
                if (f_counter >= f_duration)
                {
                    effect_Animator.SetTrigger(i_EnterEnd);
                    f_counter = 0f;
                    b_startMoving = false;
                }
                else
                {
                    // Move wave forward
                    var step = f_speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetToMove, step);
                    print("Is moving "+ transform.position + " to " + targetToMove);
                }
            }
        }
    }
    private void OnDisable()
    {
        f_counter = 0;
        b_effectFinished = false;
        b_startMoving = false;
        b_spellEffectActiveInBattlefield = false;
    }
    #endregion

    #region Inherit Methods
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
        b_spellEffectActiveInBattlefield = true;
        print(transform.name);
        // Reset size
        transform.localScale = v3_correctSizeWithNoParent;
    }

    private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = startPosition;
    }
    #endregion

    #region Tsunami Exclusive methods
    public void GetEnemiesOutOfWave()
    {
        foreach(Enemy enemyInsideWave in enemiesScripts_List)
        {
            if (enemyInsideWave != null) 
            {
                enemyInsideWave.transform.parent = null;
            }
        }
    }
    internal void StartMovingWave()
    {
        b_startMoving = true;
    }
    #endregion

    #region Colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            // Store enemies inside to later release them
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
            // Set as child to move enemies with it
            collision.transform.parent = transform;
            // Deal damage once to enemies
            Enemy enemyHitted = collision.gameObject.GetComponent<Enemy>();
            enemyHitted.ReceiveDamage((int)f_damage);
        }
    }
    #endregion
}
