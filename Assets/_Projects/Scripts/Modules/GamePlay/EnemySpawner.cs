using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _spawnEnemyId;

    private float spawnInterval => 1f;

    public async Task SpawnWave(int waveIndex)
    {
        //Validate wave index
        if (waveIndex < 0 || waveIndex >= DataManager.Instance.levelDesignData._waveList.Count)
        {
            Debug.LogError("Invalid wave index");
            return;
        }

        LevelDesign waveData = DataManager.Instance.levelDesignData._waveList[waveIndex];
        Dictionary<GameObject, int> enemySpawnCount = new Dictionary<GameObject, int>();

        for (int i = 0; i < waveData.enemyList.Count; i++)
        {
            enemySpawnCount[waveData.enemyList[i]] = 0;
        }

        await Task.Delay((int)DataManager.Instance.levelDesignData._waveList[waveIndex].waveDelay * 1000);
        await SpawnEnemies(waveData, enemySpawnCount);
    }

    private Vector3 GetRandomPositionWithinScreen()
    {
        float x = Random.Range(0.1f, 0.9f);
        float y = Random.Range(0.1f, 0.9f);
        Vector3 randomViewportPoint = new Vector3(x, y, GamePlayManager.Instance.mainCamera.nearClipPlane);
        return GamePlayManager.Instance.mainCamera.ViewportToWorldPoint(randomViewportPoint);
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
            await Task.Delay((int)spawnInterval * 1000);

            // Generate a random point around the player within the screen
            Vector3 spawnPosition = GetRandomPositionWithinScreen();
            
            
            // Select a random enemy type to spawn
            int randomIndex = Random.Range(0, waveData.enemyList.Count);
            GameObject enemyToSpawn = waveData.enemyList[randomIndex];

            // Check if the selected enemy type has reached its spawn limit
            if (enemySpawnCount[enemyToSpawn] < waveData.enemyCountList[randomIndex])
            {
                // Instantiate the enemy at the spawn position
                GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(spawnPosition.x, spawnPosition.y, 1f), Quaternion.identity);
                newEnemy.transform.parent = transform;

                // Update the spawn count for the selected enemy type
                enemySpawnCount[enemyToSpawn]++;
                totalEnemies--;
            }
        }
    }
}
