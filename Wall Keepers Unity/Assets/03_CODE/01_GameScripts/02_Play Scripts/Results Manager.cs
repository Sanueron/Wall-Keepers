using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{

    #region UI visuals
    [Header("UI Panel Variables")]
    [SerializeField] private Animator animator_ResultsPanel;
    [SerializeField] private GameObject go_newRecord, go_bestScore;
    [SerializeField] private TextMeshProUGUI tmp_currentWavesRecord, tmp_wavesCompleted,
        tmp_totalManaObtained;
    #endregion

    #region External Variables
    [Header("External References")]
    [SerializeField] RoundsManager script_roundsManager;
    #endregion

    #region Music and SFXs
    [Header("Music and Sfx")]
    [SerializeField] private AudioClip aC_resultsMusic;
    [SerializeField] private AudioClip aC_levelComplete;
    #endregion

    #region Animator Control Variables
    int i_animatorTrigger = Animator.StringToHash("Show");
    #endregion

    #region Rewards Stats
    int i_manaObtained = 0;
    #endregion

    #region UNITY LIFE CYCLE
    void Start()
    { 
        // Subscribe to events
        Enemy.onEnemyDefeated += Enemy_onEnemyDefeated;
        WallManager.e_OnWallDestroyed += WallManager_e_OnWallDestroyed;

        // Set initial Mana Energy obtained
        i_manaObtained = 0;

        // Unpause Level
        LevelsGnralManager.b_stopLevel = false;
    }
    #endregion

    #region Events Methods
    private void Enemy_onEnemyDefeated(object sender, System.EventArgs e)
    {
        Enemy defeatedEnemy = (Enemy)sender;
        i_manaObtained += defeatedEnemy.i_manaEnergyReleased;
        print(defeatedEnemy.name);
        // Save type of enemy defeated for later count of defeated enemies by type
    }
    private void WallManager_e_OnWallDestroyed(object sender, System.EventArgs e)
    {
        // Update Results Panel info
        ShowNewRecordOrBestScore();
        ShowWavesCompleted();
        ShowManaObtained();

        // Pause Enemies Actions
        LevelsGnralManager.b_stopLevel = true;

        // Play finish SFX
        AudioManager.instance.PlaySFxOneTime(aC_levelComplete);

        //Show results panel
        animator_ResultsPanel.SetTrigger(i_animatorTrigger);
        Debug.Log("finish");

        // Play Results Panel Music
        AudioManager.instance.PlayNewMusicClip(aC_resultsMusic);
    }
    #endregion

    #region Results Methods
    private void ShowNewRecordOrBestScore()
    {
        if (script_roundsManager.i_currentWave - 1 > PlayerData.i_maxRound) // New Record
        {
            // Show New Record text and hide Best Score text
            go_newRecord.SetActive(true);
            go_bestScore.SetActive(false);
        }
        else // Not best score
        {
            // Pass Best score to TMP visual
            tmp_currentWavesRecord.text = PlayerData.i_maxRound.ToString();
            // Show New Record text and hide Best Score text
            go_newRecord.SetActive(false);
            go_bestScore.SetActive(true);
        }
    }
    private void ShowWavesCompleted()
    {
        int wavesCompleted = script_roundsManager.i_currentWave - 1;
        tmp_wavesCompleted.text = wavesCompleted.ToString();
    }
    private void ShowManaObtained()
    {
        tmp_totalManaObtained.text = i_manaObtained.ToString();
    }
    #endregion

}
