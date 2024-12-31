using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;

public class UIScreenManager : Singleton<UIScreenManager>, IMessageHandle
{
    private void OnEnable()
    {
        MessageManager.Instance.AddSubcriber(NamMessageType.OnDataChanged, this);
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnDataChanged, this);
    }

    public UIScreenHUD UIScreenHUD;
    void Start()
    {
        UIScreenHUD.Show();
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case NamMessageType.OnDataChanged:
                UIScreenHUD.UpdateUI();
                break;
        }
    }
}
