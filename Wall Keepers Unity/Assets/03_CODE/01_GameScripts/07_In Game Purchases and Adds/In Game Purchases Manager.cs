using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePurchasesManager : MonoBehaviour
{
    #region UI Variables to assign in Inspector
    [SerializeField] TextMeshProUGUI tmp_playerMana;
    #endregion

    #region Control Variables
    float f_playerCurrentMana;
    #endregion

    #region Local Methods
    public void Buy5000Mana() // Price 0,99€
    {
        // Assign local var of mana obtained
        int i_manaObtained = 5000;
        // Assign Local Variable Current Mana
        f_playerCurrentMana = PlayerData.i_mana;
        // Add Mana 
        PlayerData.i_mana += i_manaObtained;
        StartCoroutine(SumUp(PlayerData.i_mana));
        // Save Data
        SaveData.instance.AssignDataAndSave();
    }
    public void Buy30000Mana() // Price 0,99€
    {
        // Assign local var of mana obtained
        int i_manaObtained = 30000;
        // Assign Local Variable Current Mana
        f_playerCurrentMana = PlayerData.i_mana;
        // Add Mana 
        PlayerData.i_mana += i_manaObtained;
        StartCoroutine(SumUp(PlayerData.i_mana));
        // Save Data
        SaveData.instance.AssignDataAndSave();
    }
    #endregion

    #region Spells Sum Up Effect Methods
    IEnumerator SumUp(float targetValue)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(targetValue - f_playerCurrentMana) / f_duration;
        while (f_playerCurrentMana != targetValue)
        {
            f_playerCurrentMana = Mathf.MoveTowards(f_playerCurrentMana, targetValue, rate * Time.deltaTime);
            tmp_playerMana.text = ((int)f_playerCurrentMana).ToString();
            yield return null;
        }
    }
    #endregion
}
