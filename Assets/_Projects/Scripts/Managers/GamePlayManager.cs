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

    public void AddEnemyToList(GameObject a)
    {
        if (!_enemyList.Contains(a))
            _enemyList.Add(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveEnemyFromList(GameObject a)
    {
        // if (!_enemyList.Contains(a))
        _enemyList.Remove(a);
    }
}
