using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScreenHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public void Show()
    {
        _scoreText.text = DataManager.Instance.gameData.score.ToString();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        _scoreText.text = DataManager.Instance.gameData.score.ToString();
    }
}
