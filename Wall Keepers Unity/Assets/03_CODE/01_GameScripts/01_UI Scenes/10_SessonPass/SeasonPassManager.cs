using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonPassManager : MonoBehaviour
{
    #region Events
    internal event EventHandler OnMagicPassLevelUpdated;
    #endregion

    #region Variables
    private int i_passMaxLevel = 50, i_passCurrentLevel = 0, 
        i_manaUsed, i_manaToUseToReachNextLevel, i_baseManaToReachNextLvl = 50;
    private float f_currentLevelBarFillAmount, f_currentPlayerManaDisplaying, f_currentPlayerManaKeysDisplaying;

    [Header("Scene UI variables")]
    [SerializeField] Image img_fillBar;
    [SerializeField] TextMeshProUGUI tmp_manaLeftToNextLevel, tmp_currentMagicPassLevel,
        tmp_playerManaEnergy, tmp_playerManaKeys;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        // Update Player Currencies
        UpdateManaEnergyCurrency();
        UpdateManaKeysCurrency();

        // Calculate current exp and current exp required for next level and then check for updating level and visuals
        i_manaUsed = PlayerData.i_magicPassManaExp;
        i_manaToUseToReachNextLevel = i_baseManaToReachNextLvl + PlayerData.i_magicPassLevel * 10;
        StartCoroutine(UpdateCurrentPassLevelAndExp());
    }
    #endregion

    #region Methods to Update lvl
    private void UpdatelvlBar()
    {
        img_fillBar.fillAmount = GetPassLevelNormalized();
    }
    public float GetPassLevelNormalized() // To use Image fill component instead of Sliders
    {
        return (float)i_passCurrentLevel / i_passMaxLevel;
    }
    public void IncreaseOneLevel()
    {
        // Increase Level
        i_passCurrentLevel++;
        // Show level increase
        StartCoroutine(IncreaseLevel(GetPassLevelNormalized()));
    }
    public void IncreaseSeveralLevels(int levelsIncreased)
    {
        // Increase Level
        i_passCurrentLevel += levelsIncreased;
        // Show level increase
        StartCoroutine(IncreaseLevel(GetPassLevelNormalized()));
    }
    IEnumerator IncreaseLevel(float f_newLevelBarFillAmount)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newLevelBarFillAmount - f_currentLevelBarFillAmount) / f_duration;
        while (f_currentLevelBarFillAmount != f_newLevelBarFillAmount)
        {
            f_currentLevelBarFillAmount = Mathf.MoveTowards(f_currentLevelBarFillAmount, f_newLevelBarFillAmount, rate * Time.deltaTime);
            img_fillBar.fillAmount = f_currentLevelBarFillAmount;
            yield return null;
        }
    }
    private void UpdateLevelAndExpVisuals()
    {
        // Visuals: Level Bar & Exp to next level Bar
        UpdatelvlBar();
        tmp_currentMagicPassLevel.text = "Level " + PlayerData.i_magicPassLevel.ToString();
        i_manaUsed = PlayerData.i_magicPassManaExp;
        tmp_manaLeftToNextLevel.text = i_manaUsed + "/" + i_manaToUseToReachNextLevel;
    }
    IEnumerator UpdateCurrentPassLevelAndExp()
    {
        // If mana used is lower than mana required to use to next level STOP
        if (i_manaUsed < i_manaToUseToReachNextLevel)
        {
            UpdateLevelAndExpVisuals();
            // Fire Event to make all Reward boxes update their state
            OnMagicPassLevelUpdated?.Invoke(this, EventArgs.Empty);
            yield return null;
        }
        while (i_manaUsed >= i_manaToUseToReachNextLevel)
        {
            // Calculate current level and current mana used and mana to use
            i_manaToUseToReachNextLevel = i_baseManaToReachNextLvl + PlayerData.i_magicPassLevel * 10;
            i_manaUsed -= i_manaToUseToReachNextLevel; // reduce mana used according to mana required to next lvl
            PlayerData.i_magicPassLevel++;
            yield return null;
        }
    }
    #endregion

    #region Update Currencies
    public void UpdateManaEnergyCurrency()
    {
        StartCoroutine(UpdateManaEnergy(PlayerData.i_mana));
    }
    public void UpdateManaKeysCurrency()
    {
        StartCoroutine(UpdateManaKeys(PlayerData.i_keys));
    }
    IEnumerator UpdateManaEnergy(float f_newManaEnergy)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newManaEnergy - f_currentPlayerManaDisplaying) / f_duration;
        while (f_currentPlayerManaDisplaying != f_newManaEnergy)
        {
            f_currentPlayerManaDisplaying = Mathf.MoveTowards(f_currentPlayerManaDisplaying, f_newManaEnergy, rate * Time.deltaTime);
            tmp_playerManaEnergy.text = f_currentPlayerManaDisplaying.ToString();
            yield return null;
        }
    }
    IEnumerator UpdateManaKeys(float f_newManaKeys)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newManaKeys - f_currentPlayerManaKeysDisplaying) / f_duration;
        while (f_currentPlayerManaKeysDisplaying != f_newManaKeys)
        {
            f_currentPlayerManaKeysDisplaying = Mathf.MoveTowards(f_currentPlayerManaKeysDisplaying, f_newManaKeys, rate * Time.deltaTime);
            tmp_playerManaKeys.text = f_currentPlayerManaKeysDisplaying.ToString();
            yield return null;
        }
    }
    #endregion
}



