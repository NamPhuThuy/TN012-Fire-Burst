using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.Serialization;
using ToggleFlow = Unity.VisualScripting.ToggleFlow;

public class DataManager : Singleton<DataManager>, IMessageHandle
{
    public PlayerData playerData;
    public LevelDesignData levelDesignData;

    private string _saveDataPath;

    private void OnEnable()
    {
        MessageManager.Instance.AddSubcriber(NamMessageType.OnEnemyDie, this);
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnEnemyDie, this);
    }

    public void LoadData()
    {
        
    }

    public bool SavaData()
    {
        return true;
    }

    public void ResetData()
    {
        
    }


    public void Handle(Message message)
    {
        switch (message.type)
        {
            case NamMessageType.OnEnemyDie:
                playerData.score++;
                MessageManager.Instance.SendMessage(new Message(NamMessageType.OnDataChanged));
                break;
        }
    }
}
