using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedWavesResultsPanels : MonoBehaviour
{
    #region Complete or Fail Variables
    [SerializeField] Animator animator_lmtWavesResultsPanel;
    private int i_animatorVariables = Animator.StringToHash("Show");
    [SerializeField] GameObject go_levelCompleteMenu;
    [SerializeField] GameObject go_levelFailedMenu;
    #endregion

    #region Rewards Control variables
    [Header("Rewards Control Variables")]
    [SerializeField] private bool b_spellRewarded;
    [SerializeField] private int i_spellID, i_spellAmount;
    [SerializeField] private bool b_elementalRunesRewarded;
    [SerializeField] private int i_runeID, i_runesAmount;
    [SerializeField] private bool b_manaKeysRewarded;
    [SerializeField] private int i_manaKeysObtained;
    #endregion

    #region Unity Life Cycle
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to events
        WallManager.e_OnWallDestroyed += WallManager_e_OnWallDestroyed;
        RoundsManager.ev_OnLevelComplete += RoundsManager_ev_OnLevelComplete;
    }
    #endregion


    #region Events Methods
    private void WallManager_e_OnWallDestroyed(object sender, EventArgs e)
    {
        LevelFailed();
    }
    private void RoundsManager_ev_OnLevelComplete(object sender, EventArgs e)
    {
        LevelComplete();
    }
    #endregion

    #region Complete or Failed Methods
    public void LevelFailed()
    {
        // Stop game
        LevelsGnralManager.b_stopLevel = true;

        // Activa Level failed panel
        go_levelFailedMenu.SetActive(true);

        // Show level failed Panel
        animator_lmtWavesResultsPanel.SetTrigger(i_animatorVariables);
    }
    private void LevelComplete()
    {
        // Stop game
        LevelsGnralManager.b_stopLevel = true;

        // Activa Level complete panel
        go_levelCompleteMenu.SetActive(true);

        // Show level complete Panel
        animator_lmtWavesResultsPanel.SetTrigger(i_animatorVariables);

        // Give Rewards and Mark reward as obtained
        // Check if Reward is spell
        if (b_spellRewarded)
        {
            // If spell obtained already directly add spell amount
            PlayerData.b_spellCardObtained[i_spellID] = true;
            PlayerData.i_spell_Cards_Obtained[i_spellID] += i_spellAmount;
        }
        if (b_elementalRunesRewarded) // Get runes reward
        {
            PlayerData.i_elementalRunesInStock[i_runeID] += i_runesAmount;
        }
        if (b_manaKeysRewarded)
        {
            PlayerData.i_keys += i_manaKeysObtained;
        }

        // Save Data
        SaveData.instance.AssignDataAndSave();
    }
    #endregion
}
