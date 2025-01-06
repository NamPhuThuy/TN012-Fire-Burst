using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _spawnEnemyId;

    //Some values
    private float SpawnIntervalTime => DataManager.Instance.levelDesignData._waveList[DataManager.Instance.playerData.currentWave].spawnInterval;
    private int WaveCount => DataManager.Instance.levelDesignData._waveList.Count;
    private int WaveIndex => DataManager.Instance.playerData.currentWave;
    

    public async Task SpawnCurrentWave()
    {
        //Validate wave index
        await Validate();

        LevelDesign waveData = DataManager.Instance.levelDesignData._waveList[WaveIndex];
        Dictionary<GameObject, int> enemySpawnCount = new Dictionary<GameObject, int>();

        for (int i = 0; i < waveData.enemyList.Count; i++)
        {
            enemySpawnCount[waveData.enemyList[i]] = 0;
        }
        
        await SpawnEnemies(waveData, enemySpawnCount);
    }

    #region Helpers

    private Vector3 GetRandomPositionWithinScreen()
    {
        float x = Random.Range(0.1f, 0.9f);
        float y = Random.Range(0.1f, 0.9f);
        Vector3 randomViewportPoint = new Vector3(x, y, GamePlayManager.Instance.mainCamera.nearClipPlane);
        return GamePlayManager.Instance.mainCamera.ViewportToWorldPoint(randomViewportPoint);
    }

    #endregion

    private async Task Validate()
    {
        if (WaveIndex < 0 || WaveIndex >= WaveCount)
        {
            Debug.LogError("Invalid wave index");
        }
    }

    private async Task SpawnEnemies(LevelDesign waveData, Dictionary<GameObject, int> enemySpawnCount)
    {
        int totalEnemies = 0;
        foreach (int count in waveData.enemyCountList)
        {
            totalEnemies += count;
        }

        while (totalEnemies > 0)
        {
            Debug.LogError($"Delay in Spawn");
            await Task.Delay((int)SpawnIntervalTime * 1000);
            Vector3 spawnPosition = GetRandomPositionWithinScreen();
            
            // Select a random enemy type to spawn
            int randomIndex;
            GameObject enemyToSpawn;

            do
            {
                randomIndex = Random.Range(0, waveData.enemyList.Count);
                enemyToSpawn = waveData.enemyList[randomIndex];
            } while (enemySpawnCount[enemyToSpawn] >= waveData.enemyCountList[randomIndex]);

            // Instantiate the enemy at the spawn position
            GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(spawnPosition.x, spawnPosition.y, 1f), Quaternion.identity);
            newEnemy.transform.parent = transform;

            // Update the spawn count for the selected enemy type
            enemySpawnCount[enemyToSpawn]++;
            totalEnemies--;
        }
    }
}
