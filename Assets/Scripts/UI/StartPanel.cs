using System;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    [SerializeField] private Button _startButton;

    public event Action StartButtonClicked;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        StartButtonClicked?.Invoke();
    }
}
