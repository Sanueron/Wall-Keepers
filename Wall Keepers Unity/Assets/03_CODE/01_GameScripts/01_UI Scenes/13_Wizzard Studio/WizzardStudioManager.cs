using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WizzardStudioManager : MonoBehaviour
{
    #region Variables
    [Header("Canvas")]
    [SerializeField] Canvas canvas_bestiary, canvas_upgradeWall, canvas_upgradeMagic;
    [SerializeField] TextMeshProUGUI text_playerMana_upgradeWall, text_playerMana_upgradeMagic;
    #endregion
    #region Unity Life Cycle
    private void Start()
    {
        text_playerMana_upgradeWall.text = PlayerData.i_mana.ToString();
        text_playerMana_upgradeMagic.text = PlayerData.i_mana.ToString();
    }
    #endregion
    #region Canvas methods
    public void CloseCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false); 
    }
    public void OpenBestiary()
    {
        canvas_bestiary.gameObject.SetActive(true); 
    }
    public void OpenUpgradeWall()
    {
        canvas_upgradeWall.gameObject.SetActive(true);
    }
    public void OpenUpgradeMagic()
    {
        canvas_upgradeMagic.gameObject.SetActive(true);
    }

    #endregion




}
