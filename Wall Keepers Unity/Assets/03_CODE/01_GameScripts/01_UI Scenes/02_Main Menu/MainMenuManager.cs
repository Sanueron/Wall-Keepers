using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Use List to reassign saved spells in deck prefabs and data when opening game")]
    [SerializeField] List<ScripteableCard> listOfSpellsData = new List<ScripteableCard>();

    [Header("Player Currencies")]
    [SerializeField] private TextMeshProUGUI tmp_playerMana;
    [SerializeField] private TextMeshProUGUI tmp_playerManaKeys;
    float f_currentManaEnergy, f_currentManaKeys;

    #region Unity Life Cycle
    private void Start()
    {
        // Set Initial Spells obtained
        SetInitialSpells();
        // Assign spells in deck to avoid bug
        ReAssignSpellsInDeckPrefabs();
        // SaveData.instance.AssignDataAndSave();
        // Update Player Mana and Keys UI visual elements
        GetPlayerCurrenciesAndUpdateTMP();
    }
    #endregion

    private void SetInitialSpells()
    {
        PlayerData.b_spellCardObtained[1] = true; // Fire ball
        PlayerData.b_spellCardObtained[2] = true; // Rock Ball
        PlayerData.b_spellCardObtained[3] = true; // Water Ball
        PlayerData.b_spellCardObtained[4] = true; // Fire Wall
        PlayerData.b_spellCardObtained[5] = true; // Rock Wall
        PlayerData.b_spellCardObtained[6] = true; // Tornado
        PlayerData.b_spellCardObtained[7] = true; // Wind Arrow
        PlayerData.b_spellCardObtained[9] = true; // Ignite
    }
    private void ReAssignSpellsInDeckPrefabs() // To avoid data loading error when opening game first time
    {
        for(int i = 0; i < PlayerData.i_spellInDeckByID.Length; i++)
        {
            if (PlayerData.i_spellInDeckByID[i] > 0) // Avoid ID 0, as its default for empty
            {
                for (int j = 0; j < listOfSpellsData.Count; j++)
                {
                    int i_checkedSpellId = listOfSpellsData[j].i_id;
                    int i_spellInDeckID = PlayerData.i_spellInDeckByID[i];

                    if (i_spellInDeckID == i_checkedSpellId)
                    {
                        // Reassign Spells Prefabs
                        PlayerData.go_spellsIndeck[i] = listOfSpellsData[j].spellPrefab;

                        // Reassign Spells Data objects
                        PlayerData.spellsInDeckData[i] = listOfSpellsData[j];
                    }
                }
            }
        }
    }

    #region Manage Player Currencies Methods
    private void GetPlayerCurrenciesAndUpdateTMP()
    {
        tmp_playerMana.text = PlayerData.i_mana.ToString();
        f_currentManaEnergy = PlayerData.i_mana;
        tmp_playerManaKeys.text = PlayerData.i_keys.ToString();
        f_currentManaKeys = PlayerData.i_keys;
    }
    internal void UpdateEffectManaEnergy(float f_newManaEnergy)
    {
        StartCoroutine(SumUPManaEnergy(f_newManaEnergy));
    }
    IEnumerator SumUPManaEnergy(float f_newManaEnergy)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newManaEnergy - f_currentManaEnergy) / f_duration;
        while (f_currentManaEnergy != f_newManaEnergy)
        {
            f_currentManaEnergy = Mathf.MoveTowards(f_currentManaEnergy, f_newManaEnergy, rate * Time.deltaTime);
            tmp_playerMana.text = ((int)f_currentManaEnergy).ToString();
            yield return null;
        }
    }

    internal void ShowSumUpManaKeys(float f_newSilverCoinsAmount)
    {
        StartCoroutine(SumUpSilverCoins(f_newSilverCoinsAmount));
    }
    IEnumerator SumUpSilverCoins(float f_newSilverCoinsAmount)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newSilverCoinsAmount - f_currentManaKeys) / f_duration;
        while (f_currentManaKeys != f_newSilverCoinsAmount)
        {
            f_currentManaKeys = Mathf.MoveTowards(f_currentManaKeys, f_newSilverCoinsAmount, rate * Time.deltaTime);
            tmp_playerManaKeys.text = ((int)f_currentManaKeys).ToString();
            yield return null;
        }
    }
    #endregion
}
