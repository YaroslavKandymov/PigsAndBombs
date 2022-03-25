using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : Panel
{
    [SerializeField] private Button _restartButton;

    public event Action RestartButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }
}
