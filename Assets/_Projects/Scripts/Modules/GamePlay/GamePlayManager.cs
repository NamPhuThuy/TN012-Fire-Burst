using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public GameObject player;
    [SerializeField] private List<GameObject> _enemyList;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance._musicGamePlay, true);
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
