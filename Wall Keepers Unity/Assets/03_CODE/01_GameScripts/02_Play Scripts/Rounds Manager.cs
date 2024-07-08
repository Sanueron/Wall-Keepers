using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    #region UI Round/Waves counter panel
    [Header("Next Wave Panel Variables")]
    [SerializeField] Animator animator_NextWave;
    [SerializeField] private TMP_Text tmp_nextWaveText;
    #endregion

    #region Animator Variables
    private int i_startTriggerHash = Animator.StringToHash("Start");
    private int i_nextWaveTriggerHash = Animator.StringToHash("Next Wave");
    #endregion

    #region Waves/Rounds count variables
    internal int i_currentWave;
    #endregion

    #region External Variables
    EnemiesManager script_enemiesManager;
    #endregion

    #region UNITY life Cycle
    private void Start()
    {
        // Get Enemies Manager script to access spawn enemies method
        GameObject go = GameObject.Find("Enemies Manager");
        if(go != null)
        {
            script_enemiesManager = go.GetComponent<EnemiesManager>();
        }

        i_currentWave = 1;
    }
    #endregion

    #region Local Methods
    public void StartEndlessWaveLevel()
    {
        animator_NextWave.SetTrigger(i_startTriggerHash);
    }
    internal void SpawnFirstWaveEnemies()
    {
        script_enemiesManager.SetSpawnEnemiesToTrue();
    }
    #endregion
}
