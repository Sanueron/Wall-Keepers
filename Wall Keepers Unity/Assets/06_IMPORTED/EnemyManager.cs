using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    #region Variables 
    public List<GameObject> enemies_gameObject;
    public GameObject battlefield_gameObject;
    private Collider2D _battlefield_Collider;
    public float spawnTime;
    public float timeBetweenWaves;
    //public int enemiesPerWave = 5;
    private int currentRound = 1;
    public int baseEnemiesPerWave;
    public int enemiesIncrementPerRound;
    public Old_ObjectPoolerScript script_enemiesPool;
    private GameObject _newEnemy_GameObject;
    private float lastSpawnedX;
    public float f_separationbtwEnemies;
    public GameObject[] spawnPoints_GameObjects;
    private int i_spawnPointNumber;
    private int i_TargetRandom;
    public List<GameObject> queueEnemies_gameObject;
    public GameObject primerObjetoActivo;
    public bool b_activeSpawnPoint;
    public bool b_allSpawnPointsActive;

    public List<Transform> enemiesTargets;
  
    #endregion
    #region Unity Life Cycle Methods
    private void Start()
    {
        _battlefield_Collider = battlefield_gameObject.GetComponent<Collider2D>();
        StartCoroutine(SpawnWaves());
        InvokeRepeating("RepeatingSpawn", 0.2f, 3.0f); // Simple Endless Game  

    }
    void FixedUpdate()
    {
        CheckSpawnPoints();
        QueueManagament();
    }

    #endregion
    #region Enemy Manager Methods
    IEnumerator SpawnWaves()
    {
        
        while (true)
        {
            //script_levelManager.UpdateRoundText(currentRound);
            currentRound++;

            int enemiesToSpawn = baseEnemiesPerWave + (currentRound - 1) * enemiesIncrementPerRound;

            // Espera antes de comenzar la siguiente oleada
            yield return new WaitForSeconds(timeBetweenWaves);


            // Genera enemigos durante la oleada
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(spawnTime); // Puedes ajustar según tus necesidades
            }

            while (HasActiveEnemies())
            {
                yield return null; // Espera hasta que no haya enemigos activos
            }
        }
    }

    void QueueManagament()
    {
        if (b_activeSpawnPoint && queueEnemies_gameObject.Count > 0)
        {
            for (int i = 0; i < spawnPoints_GameObjects.Length; i++)
            {
                if (spawnPoints_GameObjects[i].activeSelf)
                {
                    // Ensure there are elements in the queue before attempting to access the first one.
                    if (queueEnemies_gameObject.Count > 0)
                    {
                        // Move the first enemy from the queue to the enemies pool 
                        script_enemiesPool.pooledObjectsList.Add(queueEnemies_gameObject.First());

                        // Spawn the enemy.
                        SpawnEnemy();

                        // Remove the first object from the queue.
                        queueEnemies_gameObject.RemoveAt(0);

                        // Exit the loop when the first active object is found.
                        break;
                    }
                    else
                    {
                        // Handle the case where the queue is empty.
                        Debug.LogWarning("The enemy queue is empty.");
                    }
                }
            }
        }
    }

    void CheckSpawnPoints()
    {
        // Check if at least one spawn point is currently active.
        b_activeSpawnPoint = spawnPoints_GameObjects.Any(objeto => objeto.activeSelf);

        // Check if all spawn points are currently active.
        b_allSpawnPointsActive = spawnPoints_GameObjects.All(objeto => objeto.activeSelf);
    }

    void SpawnEnemy()
    {
           // Obtain an enemy from the enemies pool.
           _newEnemy_GameObject = script_enemiesPool.GetPooledObject();
            if (_newEnemy_GameObject == null) return;
            //Generate random number for spawn point
            i_spawnPointNumber = Random.Range(0, spawnPoints_GameObjects.Length);
        bool canSpawn = spawnPoints_GameObjects[i_spawnPointNumber].activeSelf;
            // If at least one spawn point is active, select a random active spawn point.
            if (b_activeSpawnPoint)
            {

                // Ensure that the selected spawn point is available for spawning.
                while (!canSpawn)
                {
                    i_spawnPointNumber = Random.Range(0, spawnPoints_GameObjects.Length);
                    canSpawn = spawnPoints_GameObjects[i_spawnPointNumber].activeSelf;
                }
                spawnPoints_GameObjects[i_spawnPointNumber].gameObject.SetActive(false);
                _newEnemy_GameObject.transform.position = spawnPoints_GameObjects[i_spawnPointNumber].transform.position;
                //Set the spawn point number to enable spawnpoint after enemy die 
                Enemy newEnemy = _newEnemy_GameObject.GetComponent<Enemy>();
                newEnemy.i_spawnNumber = i_spawnPointNumber;

                _newEnemy_GameObject.GetComponent<Enemy>().moveToTarget_transform = enemiesTargets[i_spawnPointNumber];
                _newEnemy_GameObject.SetActive(true);
            }
            else
            {
                //Add enemies that cannot be spawn to a waiting queue.
                queueEnemies_gameObject.Add(_newEnemy_GameObject);
            }
     }

    public void EnableSpawnPoint(int spawnNumber)
    {
        spawnPoints_GameObjects[spawnNumber].SetActive(true);
    }
    bool HasActiveEnemies()
    {
        // Encuentra todos los objetos con la etiqueta de enemigo
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Filtra para asegurarse de que sean enemigos activos
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                return true; // Hay al menos un enemigo activo
            }
        }

        return false; // No hay enemigos activos
    }
    //COD Zombies Waves System
    int CalculateEnemiesForRound(int round)
    {
        // Utiliza la fórmula para calcular el número de enemigos
        return Mathf.RoundToInt(0.000058f * Mathf.Pow(round, 3) + 0.074032f * Mathf.Pow(round, 2) + 0.718119f * round + 14.738699f);
    }
    // Endless Simple Game
    void RepeatingSpawn()
    {
        SpawnEnemy();
    }
    #endregion
}
