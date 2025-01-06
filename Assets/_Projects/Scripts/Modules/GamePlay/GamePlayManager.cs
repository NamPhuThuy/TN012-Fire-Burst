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
    public List<GameObject> _enemyList;
    public EnemySpawner _enemySpawner;
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
