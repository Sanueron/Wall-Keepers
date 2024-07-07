using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Manages Pool of enemies and Spawn Methods

    #region Lists of Demonitos by Ranks
    [Header("Demonitos")]
    [SerializeField] private List<Enemy> listOf_enemies_Rank_1 = new List<Enemy>();
    [SerializeField] private List<Enemy> listOf_enemies_Rank_2 = new List<Enemy>();
    [SerializeField] private List<Enemy> listOf_enemies_Rank_3 = new List<Enemy>();
    [SerializeField] private List<Enemy> listOf_enemies_Rank_4 = new List<Enemy>();
    [SerializeField] private List<Enemy> listOf_enemies_Rank_5 = new List<Enemy>();
    #endregion

    #region Spawn Points
    [Header("Spawn Points")]
    [SerializeField] private List<Transform> listOf_spawnPoints = new List<Transform>();
    private List<bool> listOf_spawnPointsBlocked = new List<bool>(); // Used to avoid spawning more than 1 enemy on the same spawn point at the same time.
    #endregion

    #region Spawn Rules
    // Start with 10 enemies of rank 1, every round get +1 enemy of next rank  and -1 of current rank
    int i_currentRank = 1, i_currentRankEnemies = 10, i_nextRankEnemies = 0;
    #endregion

    #region Methods to Spawn Enemies
    internal void SpawnEnemies(int i_numberOfEnemiesSpawn)
    {
        for (int i = 0; i < i_numberOfEnemiesSpawn; i++)
        {
            switch (i_currentRank)
            {
                case 1: // Main Enemies are of Rank 1
                    // Get Random number
                    int i_random = Random.Range(0, listOf_enemies_Rank_1.Count);
                    // Check if enemy not spawned already
                    if (!listOf_enemies_Rank_1[i_random].gameObject.activeSelf)
                    {
                        // Get Spawn Point
                        int i_spawnPointIndex = Random.Range(0, listOf_spawnPoints.Count);
                        // Check if Spawn point not blocked
                        if (!listOf_spawnPointsBlocked[i_spawnPointIndex])
                        {
                            // Spawn point not blocked
                            // Place enemy in spawn point
                            listOf_enemies_Rank_1[i_random].transform.position =
                                listOf_spawnPoints[i_spawnPointIndex].transform.position;
                            // Activate Enemy
                            listOf_enemies_Rank_1[i_random].gameObject.SetActive(true);
                            // Block Spawn Point
                            listOf_spawnPointsBlocked[i_spawnPointIndex] = true;
                            // Start coroutine to reset spawn points
                            StartCoroutine(ResetSpawnPoint(i_spawnPointIndex));
                        }
                        else // Spawn point blocked
                        {

                        }
                    }
                    else
                    {
                        // Instantiate enemy of that type and added to list
                    }
                    break;

                case 2: // Main Enemies are of Rank 2
                    break;

                case 3: // Main Enemies are of Rank 3
                    break;

                case 4: // Main Enemies are of Rank 4
                    break;

                case 5: // Main Enemies are of Rank 5
                    break;
            }
        }
    }
    #endregion

    #region Reset Methods
    IEnumerator ResetSpawnPoint(int i_spawnPointIndex)
    {
        float f_time = 1f;
        yield return new WaitForSeconds(f_time);
        listOf_spawnPointsBlocked[i_spawnPointIndex] = false;
    }
    #endregion
}
