using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NamPhuThuy;
using UnityEngine;

public class DelaySpawnState : ComponentState
{
    private float DelayTime => DataManager.Instance.levelDesignData._waveList[DataManager.Instance.playerData.currentWave].waveDelay;
    
    private async void OnEnable()
    {
        await DelaySpawn();
    }

    async Task DelaySpawn()
    {
        //wait for a specific of time before spawn enemies
        Debug.LogWarning($"GameLoop - delay time: {DelayTime * 1000}");
        await Task.Delay((int)DelayTime * 1000);
        ChangeState(Next());
    }
}
