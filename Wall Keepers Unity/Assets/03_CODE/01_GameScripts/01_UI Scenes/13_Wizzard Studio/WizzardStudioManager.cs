using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WizzardStudioManager : MonoBehaviour
{
    #region Variables
    [Header("Main UI Elements")]
    [SerializeField] TextMeshProUGUI tmp_player_manaEnergy;
    [SerializeField] TextMeshProUGUI tmp_player_manaKeys;

    int i_animatorsString = Animator.StringToHash("Visible");
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        UpdateCurrencies();
    }
    #endregion

    #region Currencies Related Methods
    private void UpdateCurrencies()
    {
        tmp_player_manaEnergy.text = PlayerData.i_mana.ToString();
        tmp_player_manaKeys.text = PlayerData.i_keys.ToString();
    }
    #endregion

    public void ShowIndicatedPanel(Animator selectedPanel)
    {
        selectedPanel.SetBool(i_animatorsString, true);
    }
    public void HideIndicatedPanel(Animator selectedPanel)
    {
        selectedPanel.SetBool(i_animatorsString, false);
    }
}
