using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScreenGameOver : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeResultText;
    
    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    
    public void Show(bool isWin)
    {
        gameObject.SetActive(true);
        _scoreText.text = DataManager.Instance.playerData.score.ToString();
        _timeResultText.text = $"Time: {TimeHelper.ConvertSecondsToTimeFormat((int) Time.time)}";
        
        if (isWin)
        {
            _titleText.text = "You Win!";
        }
        else
        {
            _titleText.text = "Game Over!";
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        _restartButton.onClick.AddListener(OnClickRestart);
    }

    private void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
