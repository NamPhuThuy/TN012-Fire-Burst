using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScreenManager : Singleton<UIScreenManager>, IMessageHandle
{
    public UIScreenHUD UIScreenHUD;
    public UIScreenGameOver UIScreenGameOver;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        MessageManager.Instance.AddSubcriber(NamMessageType.OnDataChanged, this);
        MessageManager.Instance.AddSubcriber(NamMessageType.OnGameLose, this);
        MessageManager.Instance.AddSubcriber(NamMessageType.OnGameWin, this);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnDataChanged, this);
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnGameLose, this);
        MessageManager.Instance.RemoveSubcriber(NamMessageType.OnGameWin, this);
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
                UIScreenHUD.Show();
                break;
        }
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
