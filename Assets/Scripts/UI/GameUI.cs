using System;
using UnityEngine;

public class GameUI : MonoBehaviour, IRestartable
{
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private GamePanel _gamePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;

    public event Action StartButtonClicked;
    public event Action RestartButtonClicked;
    public event Action ExitButtonClicked;

    private void OnEnable()
    {
        _startPanel.StartButtonClicked += OnStartButtonClicked;
        _losePanel.RestartButtonClicked += OnRestartButtonClicked;
        _winPanel.RestartButtonClicked += OnRestartButtonClicked;
        _winPanel.ExitButtonClicked += OnExitButtonClicked;
    }

    private void OnDisable()
    {
        _startPanel.StartButtonClicked -= OnStartButtonClicked;
        _losePanel.RestartButtonClicked -= OnRestartButtonClicked;
        _winPanel.RestartButtonClicked -= OnRestartButtonClicked;
        _winPanel.ExitButtonClicked -= OnExitButtonClicked;
    }

    private void Start()
    {
        _startPanel.Open();

        _gamePanel.Close();
        _losePanel.Close();
        _winPanel.Close();
    }

    public void OpenGamePanel()
    {
        _startPanel.Close();
        _gamePanel.Open();
    }

    public void OpenWinPanel()
    {
        _gamePanel.Close();
        _winPanel.Open();
    }

    public void OpenLosePanel()
    {
        _gamePanel.Close();
        _losePanel.Open();
    }

    private void OnStartButtonClicked()
    {
        StartButtonClicked?.Invoke();
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }

    private void OnExitButtonClicked()
    {
        ExitButtonClicked?.Invoke();
    }

    public void Restart()
    {
        _winPanel.Close();
        _losePanel.Close();
        _gamePanel.Open();
    }
}
