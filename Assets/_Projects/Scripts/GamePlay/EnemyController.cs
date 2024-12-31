using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnEnable()
    {
        GamePlayManager.Instance.AddEnemyToList(gameObject);
    }

    private void OnDisable()
    {
        GamePlayManager.Instance.RemoveEnemyFromList(gameObject);
    }

    public void DieProcess()
    {
        Destroy(gameObject);
    }
}
