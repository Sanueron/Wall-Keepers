using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryManager : MonoBehaviour
{
    #region Variables
    [SerializeField]  TextMeshProUGUI tmp_description, tmp_range,tmp_hp,tmp_attack,tmp_speed;
    [SerializeField] Image img_demonito;
    #endregion

    #region Methods
    public void ShowEnemyData(ScripteableEnemies enemyData)
    {
        img_demonito.sprite = enemyData.sprite_bestiary;
        tmp_description.text = enemyData.Description;
        tmp_hp.text = enemyData.i_hp.ToString();
        tmp_attack.text = enemyData.i_dmg.ToString();
        tmp_speed.text = enemyData.i_speed.ToString();

        if (enemyData.f_RageOfAction > 3.0f)
        {
            tmp_range.text = "RANGED";
        }
        else
        {
            tmp_range.text = "MELEE";
        }
    }
    #endregion






}
