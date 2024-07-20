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
        // Stop game
        LevelsGnralManager.b_stopLevel = true;

        // Activa Level failed panel
        go_levelFailedMenu.SetActive(true);

        // Show level failed Panel
        print("Show level faild panel");
        animator_lmtWavesResultsPanel.SetTrigger(i_animatorVariables);
    }
    private void RoundsManager_ev_OnLevelComplete(object sender, EventArgs e)
    {
        // Stop game
        LevelsGnralManager.b_stopLevel = true;

        // Activa Level complete panel
        go_levelCompleteMenu.SetActive(true);

        // Show level complete Panel
        print("Show level complete panel");
        animator_lmtWavesResultsPanel.SetTrigger(i_animatorVariables);
    }
    #endregion
}
