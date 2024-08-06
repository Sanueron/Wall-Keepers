using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardStudioManager : MonoBehaviour
{
    #region Variables
    [Header("Canvas")]
    [SerializeField] Canvas canvas_bestiary, canvas_upgradeWall, canvas_upgradeMagic;
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
