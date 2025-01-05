using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;

public class UIScreenManager : Singleton<UIScreenManager>, IMessageHandle
{
    public UIScreenHUD UIScreenHUD;
    public UIScreenGameOver UIScreenGameOver;
    
    private void OnEnable()
    {
        MessageManager.Instance.AddSubcriber(NamMessageType.OnDataChanged, this);
        MessageManager.Instance.AddSubcriber(NamMessageType.OnGameLose, this);
        MessageManager.Instance.AddSubcriber(NamMessageType.OnGameWin, this);
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnDataChanged, this);
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnGameLose, this);
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnGameWin, this);
    }

    
    void Start()
    {
        UIScreenHUD.Show();
    }

    public void Handle(Message message)
    {
        Debug.Log($"UIManager: Handle message {message.type.ToString()}");
        switch (message.type)
        {
            case NamMessageType.OnDataChanged:
                UIScreenHUD.UpdateUI();
                break;
            case NamMessageType.OnGameLose:
                UIScreenGameOver.Show(false);
                break;
            case NamMessageType.OnGameWin:
                UIScreenGameOver.Show(true);
                break;
        }
    }
}
