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

    #region Rewards Stats
    int i_manaObtained;
    #endregion

    // Start is called before the first frame update
    void Start()
    { 
        // Subscribe to events
        Enemy.onEnemyDefeated += Enemy_onEnemyDefeated;
        WallManager.e_OnWallDestroyed += WallManager_e_OnWallDestroyed;
    }

    #region Events Methods
    private void Enemy_onEnemyDefeated(object sender, System.EventArgs e)
    {
        Enemy defeatedEnemy = (Enemy)sender;
        i_manaObtained += defeatedEnemy.i_manaEnergyReleased;
        print("Voy por aqui");
        // Save type of enemy defeated for later count of defeated enemies by type
    }
    private void WallManager_e_OnWallDestroyed(object sender, System.EventArgs e)
    {
        //Show results 

        Debug.Log("finish");
    }
    #endregion

}
