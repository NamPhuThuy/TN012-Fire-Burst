using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemyState : ComponentState
{
    [SerializeField] private DelaySpawnState _delaySpawnState;
    private async void OnEnable()
    {
        await GamePlayManager.Instance._enemySpawner.SpawnCurrentWave();
        
        DataManager.Instance.playerData.currentWave++;

        if (DataManager.Instance.playerData.currentWave < DataManager.Instance.levelDesignData.maxWave)
        {
            ChangeState(_delaySpawnState.gameObject);
        }
        else
        {
            ChangeState(Next());
        }
    }
}
