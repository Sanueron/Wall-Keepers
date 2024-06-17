using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    #region Unity Life Cycle
    private void Start()
    {
        SaveData.instance.AssignDataAndSave();
    }
    #endregion
}
