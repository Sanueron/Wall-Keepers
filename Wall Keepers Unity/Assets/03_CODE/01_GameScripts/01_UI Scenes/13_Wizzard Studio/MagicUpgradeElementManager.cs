using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicUpgradeElementManager : MonoBehaviour
{
    #region Prefab Variables
    [Header("Upgrade Levels Visual Items")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject[] arrayOf_blockedLevelsIcons = new GameObject[8];
    [SerializeField] private GameObject[] arrayOf_unblockedLevelsIcons = new GameObject[8];
    [SerializeField] private GameObject[] arrayOf_upgradedLevelsIcons = new GameObject[8];
    [SerializeField] private TextMeshProUGUI tmp_upgradeCost;
    [SerializeField] private TextMeshProUGUI[] tmp_upgradesCurrentValues = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI[] tmp_upgradesIncreaseValue = new TextMeshProUGUI[4];
    #endregion

    #region Control Variables
    int i_magicMaxUpgradeLevel = 8, i_upgradeBaseCost = 5000, i_levelToUpgrade;
    [Header("Control Variables")]
    [SerializeField] int i_magicID;
    [SerializeField] private float[] arrayOf_amountToUpgradeByUpgrade = new float[8];
    int i_upgradeType;
    float f_upgradeAmount;
    #endregion

    #region External References
    [Header("External References")]
    [SerializeField] WizzardStudioManager script_wizzardStudioManager;
    #endregion

    #region UNITY LIFE CYCLE
    private void Start()
    {
        // Check current upgrades completed
        CheckCurrentUpgrades();
        // Update Visuals at start
        UpdateUpgradeCosts();
        UpdateUpgradeStatsValues();
    }
    #endregion

    private void CheckCurrentUpgrades()
    {
        for (int i = 0; i <= PlayerData.i_magicUpgradeLevel[i_magicID]; i++)
        {
            // Check if level is unblocked to upgrade
            if(i == PlayerData.i_magicUpgradeLevel[i_magicID]+1)
            {
                arrayOf_blockedLevelsIcons[i].SetActive(false);
                arrayOf_unblockedLevelsIcons[i].SetActive(true);
                // Set as next level to upgrade
                i_levelToUpgrade = i;
                break;
            }
            else if (i < PlayerData.i_magicUpgradeLevel[i_magicID])
            {
                arrayOf_blockedLevelsIcons[i].SetActive(false);
                arrayOf_unblockedLevelsIcons[i].SetActive(false);
                arrayOf_upgradedLevelsIcons[i].SetActive(true);
            }
        }

        // Additional Check for Level 0 cases
        if(i_levelToUpgrade == 0)
        {
            i_levelToUpgrade = 1;
            arrayOf_blockedLevelsIcons[0].SetActive(false);
            arrayOf_unblockedLevelsIcons[0].SetActive(true);
        }
    }

    private void UpdateUpgradeCosts()
    {
        tmp_upgradeCost.text = (i_upgradeBaseCost * i_levelToUpgrade).ToString();
    }

    private void UpdateUpgradeStatsValues()
    {
        // Update current increase obtained

        // Update Damage Increased
        tmp_upgradesCurrentValues[0].text = PlayerData.i_magicDamageAttributeUpgradeAmount[i_magicID].ToString();
        // Update Resistance Increased
        tmp_upgradesCurrentValues[1].text = PlayerData.i_magicResistanceAttributeUpgradeAmount[i_magicID].ToString();
        // Update Duration Increased
        tmp_upgradesCurrentValues[2].text = PlayerData.f_magicDurationAttributeUpgradedAmount[i_magicID].ToString();
        // Update Speed Increased
        tmp_upgradesCurrentValues[3].text = PlayerData.f_magicSpeedAttributeUpgradedAmount[i_magicID].ToString();

        // Update current level upgrade visual texts
        for (int i = 0; i < tmp_upgradesIncreaseValue.Length; i++)
        {
            // Set all off
            tmp_upgradesIncreaseValue[i].gameObject.SetActive(false);
            // Check if this is the attribute increased in this upgrade
            int indexOfIncreaseValueToShow = PlayerData.i_magicUpgradeLevel[i_magicID] +1;
            switch (indexOfIncreaseValueToShow)
            {
                case 1: // Damage
                    tmp_upgradesIncreaseValue[0].gameObject.SetActive(true);
                    break;
                case 2: // Resistance
                    tmp_upgradesIncreaseValue[1].gameObject.SetActive(true);
                    break;
                case 3: // Duration
                    tmp_upgradesIncreaseValue[2].gameObject.SetActive(true);
                    break;
                case 4: // Speed
                    tmp_upgradesIncreaseValue[3].gameObject.SetActive(true);
                    break;
                case 5: // Damage
                    tmp_upgradesIncreaseValue[0].gameObject.SetActive(true);
                    break;
                case 6: // Resistance
                    tmp_upgradesIncreaseValue[1].gameObject.SetActive(true);
                    break;
                case 7: // Duration
                    tmp_upgradesIncreaseValue[2].gameObject.SetActive(true);
                    break;
                case 8: // Speed
                    tmp_upgradesIncreaseValue[3].gameObject.SetActive(true);
                    break;
                case 9: // SHOW MAX LEVEL REACHED
                    break;
            }
            // Display attribute upgrade increase value in TMP
            float f_increaseValue = arrayOf_amountToUpgradeByUpgrade[PlayerData.i_magicUpgradeLevel[i_magicID]];
            tmp_upgradesIncreaseValue[i].text = "+" + f_increaseValue.ToString();
        }
        // Show next Upgrade increase
    }

    #region Magics Upgrades Methods
    /* All Magics have 8 possible upgrades
     * Level 1 & 5: Damage +50 / +100
     * Level 2 & 6: Resistance +50 / +100
     * Level 3 & 7: Duration +0.5 / +1
     * Level 4 & 8: Speed +0.2 / +0.3
    */

    public void TryUpgrade()
    {
        int i_upgradeLevelCost = i_upgradeBaseCost * i_levelToUpgrade;
        if(PlayerData.i_mana >= i_upgradeLevelCost)
        {
            // Reduce Player Mana by upgrade Cost
            PlayerData.i_mana -= i_upgradeLevelCost;
            script_wizzardStudioManager.ShowManaEnergyReduced(PlayerData.i_mana);

            // Upgrade Magic
            UpgradeMagic();
        }
        else
        {
            script_wizzardStudioManager.ShowNotEnoughManaEnergyPanel();
        }
    }
    private void UpgradeMagic()
    {
        // Increase Magic Upgrade Level
        PlayerData.i_magicUpgradeLevel[i_magicID] += 1;
        if (PlayerData.i_magicUpgradeLevel[i_magicID] >= i_magicMaxUpgradeLevel)
        {
            PlayerData.i_magicUpgradeLevel[i_magicID] = i_magicMaxUpgradeLevel;
            // Hide next increase values
            print("Hide next increase values and show max increased");
        }
        else // If not max level
        {
            // Unblock Next Level if not reached max level (Level index is -1)
            int i_thisLevelIndex = PlayerData.i_magicUpgradeLevel[i_magicID] - 1; // get this level index
            // Update visual stars icons
            arrayOf_unblockedLevelsIcons[i_thisLevelIndex].SetActive(false);
            arrayOf_upgradedLevelsIcons[i_thisLevelIndex].SetActive(true); 
            int i_nextLevelIndex = PlayerData.i_magicUpgradeLevel[i_magicID]; // get next level index
            arrayOf_blockedLevelsIcons[i_nextLevelIndex].SetActive(false);
            arrayOf_unblockedLevelsIcons[i_nextLevelIndex].SetActive(true);
            // Set next level upgrade values
            UpdateUpgradeStatsValues();
        }

        // Show Upgrade obtained panel
        //script_wizzardStudioManager

        // Add Upgrade
        int i_indexOfIncreaseAmount = i_levelToUpgrade - 1;
        switch (i_levelToUpgrade)
        {
            case 1: // Damage
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.i_magicDamageAttributeUpgradeAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 2: // Resistance
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.i_magicResistanceAttributeUpgradeAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 3: // Duration
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.f_magicDurationAttributeUpgradedAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 4: // Speed
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.f_magicSpeedAttributeUpgradedAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 5: // Damage
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.i_magicDamageAttributeUpgradeAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 6: // Resistance
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.i_magicResistanceAttributeUpgradeAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 7: // Duration
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.f_magicDurationAttributeUpgradedAmount[i_magicID] += (int)f_upgradeAmount;
                break;
            case 8: // Speed
                f_upgradeAmount = arrayOf_amountToUpgradeByUpgrade[i_indexOfIncreaseAmount];
                PlayerData.f_magicSpeedAttributeUpgradedAmount[i_magicID] += (int)f_upgradeAmount;
                break;
        }

        // Set new Level To Upgrade
        i_levelToUpgrade = PlayerData.i_magicUpgradeLevel[i_magicID];



        // Save Data
        SaveData.instance.AssignDataAndSave();
    }
    #endregion
}
