using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Use List to reassign saved spells in deck prefabs and data when opening game")]
    [SerializeField] List<ScripteableCard> listOfSpellsData = new List<ScripteableCard>();

    #region Unity Life Cycle
    private void Start()
    {
        // Set Initial Spells obtained
        SetInitialSpells();
        // Assign spells in deck to avoid bug
        ReAssignSpellsInDeckPrefabs();
        // SaveData.instance.AssignDataAndSave();
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
}
