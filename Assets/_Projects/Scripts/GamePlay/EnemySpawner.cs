using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private float spawnRadius = 7.5f;
    private float minDistance = 4.5f;
    private float maxDistance = 7.5f;

    private float spawnInterval => 1f;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);


            // Get the player's position
            Vector3 playerPosition = GamePlayManager.Instance.player.transform.position;

            // Generate a random point around the player within the spawn radius
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * maxDistance;

            // Instantiate the enemy at the spawn position
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 1f), Quaternion.identity);

            newEnemy.transform.parent = transform;
        }
    }
}
