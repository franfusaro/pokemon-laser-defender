using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int currentWaveIndex = 0;
    int currentWaveDeadEnemies = 0;
    bool stopGame = false;

    EnemySpawner enemySpawner;
    void Awake()
    {
        SetUpSingleton();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopGame)
        {
            if (enemySpawner.IsWaveFinished(currentWaveIndex, currentWaveDeadEnemies))
            {
                currentWaveIndex++;
                currentWaveDeadEnemies = 0;
                if (enemySpawner.HasMoreWaves(currentWaveIndex))
                {
                    StartCoroutine(enemySpawner.SpawnWave(currentWaveIndex));
                }
                else
                {
                    FindObjectOfType<SceneLoader>().LoadWin();
                    stopGame = true;
                }
            }
        }
    }

    public void AddDeadEnemy()
    {
        currentWaveDeadEnemies++;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
