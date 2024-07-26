using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMissionsManager : MonoBehaviour
{
    // Manages current state of daily missions and daily reset

    #region Daily Missions variables
    // 1 - Play one game; 2 - Play 3 games; 3 - Make one pull; 4 - Improve one card; 5 complete all missions.
    [SerializeField] MissionsBoxes script_dailyMission_1, script_dailyMission_2, script_dailyMission_3, script_dailyMission_4,
        script_dailyMission_5;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        
    }
    #endregion

    private void CheckIfNewDayAndUpdateDailyMissions()
    {
        if (PlayerData.i_lastDayLogInMainMenu != DateTime.Now.Day) // Is new Day, refresh shop items
        {
            ResetAllDailyMissions();
            PlayerData.i_lastDayLogInMainMenu = DateTime.Now.Day; // Save New day as last day log in
        }
        else // Not new day
        {

        }
    }

    private void ResetAllDailyMissions()
    {
        PlayerData.i_gamesPlayed_DailyMissionCount = 0;
        PlayerData.b_makeOnePull_DailyMissionComplete = false;
        PlayerData.b_improveOneSpell_DailyMissionComplete = false;
        PlayerData.b_allDailyMissionComplete = false;

        SaveData.instance.AssignDataAndSave();
    }

    private void SetDailyMissionsState()
    {
        // For Mission Five
        bool b_allMissionsComplete = true;

        // Mission 1 - Play 1 Game
        int i_missionOneGamesPlayedRequired = 1;
        if(PlayerData.i_gamesPlayed_DailyMissionCount >= i_missionOneGamesPlayedRequired)
        {
            // Check if mission already claimed
            if (PlayerData.b_dailyMission_One_Claimed) // Show claimed already
            {
                script_dailyMission_1.go_claimedEffect.SetActive(true);
            }
            else // Show can claim
            {
                script_dailyMission_1.go_canClaimEffect.SetActive(true);
                script_dailyMission_1.button_missionClaim.interactable = true;
            }
        }
        else
        {
            b_allMissionsComplete = false;
        }

        // Mission 2 - Play 3 Games
        int i_missionTwoGamesPlayedRequired = 3;
        if (PlayerData.i_gamesPlayed_DailyMissionCount >= i_missionTwoGamesPlayedRequired)
        {
            // Check if mission already claimed
            if (PlayerData.b_dailyMission_Two_Claimed) // Show claimed already
            {
                script_dailyMission_2.go_claimedEffect.SetActive(true);
            }
            else // Show can claim
            {
                script_dailyMission_2.go_canClaimEffect.SetActive(true);
                script_dailyMission_2.button_missionClaim.interactable = true;
            }
        }
        else
        {
            b_allMissionsComplete = false;
        }

        // Mission 3 - Make 1 Pull
        if (PlayerData.b_makeOnePull_DailyMissionComplete)
        {
            // Check if mission already claimed
            if (PlayerData.b_dailyMission_Three_Claimed) // Show claimed already
            {
                script_dailyMission_3.go_claimedEffect.SetActive(true);
            }
            else // Show can claim
            {
                script_dailyMission_3.go_canClaimEffect.SetActive(true);
                script_dailyMission_3.button_missionClaim.interactable = true;
            }
        }
        else
        {
            b_allMissionsComplete = false;
        }

        // Mission 4 - Improve 1 spell
        if (PlayerData.b_improveOneSpell_DailyMissionComplete)
        {
            // Check if mission already claimed
            if (PlayerData.b_dailyMission_Four_Claimed) // Show claimed already
            {
                script_dailyMission_4.go_claimedEffect.SetActive(true);
            }
            else // Show can claim
            {
                script_dailyMission_4.go_canClaimEffect.SetActive(true);
                script_dailyMission_4.button_missionClaim.interactable = true;
            }
        }
        else
        {
            b_allMissionsComplete = false;
        }

        // Mission 5 - Complete all daily missions
        PlayerData.b_allDailyMissionComplete = b_allMissionsComplete;
        if (PlayerData.b_improveOneSpell_DailyMissionComplete)
        {
            // Check if mission already claimed
            if (PlayerData.b_dailyMission_Four_Claimed) // Show claimed already
            {
                script_dailyMission_4.go_claimedEffect.SetActive(true);
            }
            else // Show can claim
            {
                script_dailyMission_4.go_canClaimEffect.SetActive(true);
                script_dailyMission_4.button_missionClaim.interactable = true;
            }
        }
    }
}
