using System;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : Panel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public event Action RestartButtonClicked;
    public event Action ExitButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }
    
    private void OnExitButtonClicked()
    {
        ExitButtonClicked?.Invoke();
    }
}
