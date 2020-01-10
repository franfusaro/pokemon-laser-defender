using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWave(0));
        }
        while (looping);
    }

    public IEnumerator SpawnWave(int waveIndex)
    {
        var currentWave = waveConfigs[waveIndex];
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            newEnemy.GetComponent<Enemy>().SetWave(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    public bool IsWaveFinished(int waveIndex, int deadEnemies)
    {
        return waveConfigs[waveIndex].GetNumberOfEnemies() == deadEnemies;
    }

    public bool HasMoreWaves(int waveIndex)
    {
        return waveConfigs.Count > waveIndex;
    }
}
