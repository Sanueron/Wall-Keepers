using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPassRewardBox : MonoBehaviour
{
    [Header("Control Variables")]
    [SerializeField] private int i_passLevel;
    private enum AccessType { Free, Premium}
    [SerializeField] private AccessType e_accesstype;
    private enum RewardType { Mana, Keys }
    [SerializeField] private RewardType e_rewardType;
    [SerializeField] private int i_rewardAmount;

    [Header("UI Box Variables")]
    [SerializeField] private GameObject go_claimedIcon;
    [SerializeField] private GameObject go_lvlNotReachedPanel, go_premiumRewardBlockPanel;

    [Header("External References")]
    [SerializeField] private SeasonPassManager script_seasonPassManager;


    #region UNITY LIFE CYCLE
    private void Start()
    {
        // Subscribe to events
        script_seasonPassManager.OnMagicPassLevelUpdated += Script_seasonPassManager_OnMagicPassLevelUpdated;
    }
    #endregion

    #region Event Calls
    private void Script_seasonPassManager_OnMagicPassLevelUpdated(object sender, System.EventArgs e)
    {
        UpdateState();
        print(i_passLevel);
    }
    #endregion
    #region Local Methods
    private void UpdateState()
    {
        switch (e_accesstype)
        {
            case AccessType.Free:
                go_premiumRewardBlockPanel.SetActive(false);
                break;

            case AccessType.Premium:
                // Check if Premium Pass Purchased
                break;
        }

        // Check if Player reached this level in pass
        if(i_passLevel <= PlayerData.i_magicPassLevel)
        {
            go_lvlNotReachedPanel.SetActive(false);
            // Check if reward already claimed
            if (PlayerData.b_magicPassRewardClaimed[i_passLevel])
            {
                MarkRewardAsClaimed();
            }
        }
    }
    private void MarkRewardAsClaimed()
    {
        go_claimedIcon.SetActive(true);
    }
    // Method Use in Button
    public void ClaimReward()
    {
        // Mark Reward as Claimed
        PlayerData.b_magicPassRewardClaimed[i_passLevel] = true;
        // Update UI visual state of Reward
        MarkRewardAsClaimed();
        // Give reward
        switch (e_rewardType)
        {
            case RewardType.Mana:
                PlayerData.i_mana += i_rewardAmount;
                break;

            case RewardType.Keys:
                PlayerData.i_keys += i_rewardAmount;
                break;
        }

        SaveData.instance.AssignDataAndSave();
    }
    #endregion
}
