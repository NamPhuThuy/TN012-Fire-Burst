using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;

public class GameEndState : ComponentState
{
    private void Update()
    {
        if (GamePlayManager.Instance._enemyList.Count == 0)
        {
            MessageManager.Instance.SendMessage(new Message(NamMessageType.OnGameWin));
        }
    }
}
