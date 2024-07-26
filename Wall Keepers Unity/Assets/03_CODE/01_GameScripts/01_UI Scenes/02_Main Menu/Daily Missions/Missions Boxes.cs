using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionsBoxes : MonoBehaviour
{
    // Missions Visual Elements and Local logics

    #region Visual Elements
    [Header("Visual Elements")]
    [SerializeField] internal TextMeshProUGUI tmp_missionsCompletedText;
    [SerializeField] internal GameObject go_canClaimEffect, go_claimedEffect;
    #endregion

    #region Logic Elements
    [Header("Logic Elements")]
    [SerializeField] internal Button button_missionClaim;

    #endregion


    #region Local Methods
    internal void SetMissionText(int i_currentCompleted) // Rewrite mission with current completed updated
    {
        tmp_missionsCompletedText.text = i_currentCompleted.ToString();
    }
    #endregion

    #region Claim Mission Rewards methods
    public void ClaimManaReward(int i_manaRewarded)
    {
        PlayerData.i_mana += i_manaRewarded;
    }
    public void ClaimManaKeysRewardes(int i_keysRewarded)
    {
        PlayerData.i_keys += i_keysRewarded;
    }
    #endregion
}
