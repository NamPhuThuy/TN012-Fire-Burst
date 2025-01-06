using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public GameObject player;
    public Camera mainCamera;
    
    [Header("Level Design")]
    [SerializeField] private List<GameObject> _enemyList;
    [SerializeField] private EnemySpawner _enemySpawner;
    public GameObject gameLoop;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
        player = null;
        mainCamera = null;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        AudioManager.Instance.PlayMusic(AudioManager.Instance._musicGamePlay, true);
        GameLoop();
    }
    
    private void OnDisable()
    {
        //Stop all music 
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;  // Get the name of the loaded scene

        // Choose and play the appropriate music based on sceneName
        switch (sceneName)
        {
            case "MainMenu":
               
                break;
            case "GamePlay":
                gameLoop.SetActive(true);
                break;
        }
    }

    private async void GameLoop()
    {
        while (true)
        {
            if (DataManager.Instance.playerData.currentWave == DataManager.Instance.levelDesignData.maxWave && _enemyList.Count == 0)
            {
                //game over
                MessageManager.Instance.SendMessage(new Message(NamMessageType.OnGameWin));
                return;
            }
        
            await StartNewWave(DataManager.Instance.playerData.currentWave);
        }
           
    }
    

    public async Task StartNewWave(int waveIndex)
    {
        Debug.Log($"Start wave {waveIndex}");
        
        //order the EnemySpawner to spawn new wave with the wave's data
        await _enemySpawner.SpawnWave(waveIndex);
        
        DataManager.Instance.playerData.currentWave++;
    }

    public void AddEnemyToList(GameObject a)
    {
        if (!_enemyList.Contains(a))
            _enemyList.Add(a);
    }

    public void RemoveEnemyFromList(GameObject a)
    {
        // if (!_enemyList.Contains(a))
        _enemyList.Remove(a);
    }
}
