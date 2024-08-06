using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestiaryManager : MonoBehaviour
{
    #region Variables
    [SerializeField]  TextMeshProUGUI tmp_description;
    #endregion

    #region Methods

    public void ShowEnemyData(ScripteableEnemies enemyData)
    {
        tmp_description.text = enemyData.i_dmg.ToString();
    }

    #endregion






}
