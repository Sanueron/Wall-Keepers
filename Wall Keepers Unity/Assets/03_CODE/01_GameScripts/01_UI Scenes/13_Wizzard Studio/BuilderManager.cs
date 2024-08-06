using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject go_ContextualMessage_mana, go_ContextualMessage_confirmation;
    #endregion
    private void Start()
    {
        PlayerData.i_mana += 100000;
        SaveData.instance.SaveToJson();
    }
    #region Methods
    public void DisableMessage(GameObject message)
    {
        message.SetActive(false);
    }
    public void ShowManaMessage()
    {
        go_ContextualMessage_mana.SetActive(true);
    }
    public void ShowConfirmationMessage() 
    {
        go_ContextualMessage_confirmation.SetActive(true);
    }
    public void UpgradeWall( float cost)
    {
        if(PlayerData.i_mana >= cost)
        {
            //PlayerData.f_wallHP += 1000f;
        }
        else
        {
            ShowManaMessage();
        }
    }
    #endregion
}
