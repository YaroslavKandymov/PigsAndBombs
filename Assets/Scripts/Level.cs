using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelStarter _levelStarter;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private NavMeshSurface2d _navMeshSurface2d;
    [SerializeField] private FarmerHealthBar _farmerHealthBar;

    private Player _player;
    private Farmer _farmer;
    private Dog _dog;
    private RestartManager _restartManager;

    private void OnEnable()
    {
        _gameUI.StartButtonClicked += OnStartButtonClicked;
        _gameUI.RestartButtonClicked += OnRestartButtonClicked;
        _gameUI.ExitButtonClicked += OnExitButtonClicked;
    }

    private void OnDisable()
    {
        _gameUI.StartButtonClicked -= OnStartButtonClicked;
        _gameUI.RestartButtonClicked -= OnRestartButtonClicked;
        _gameUI.ExitButtonClicked -= OnExitButtonClicked;

        _player.Died -= OnPlayerDied;
        _farmer.Died -= OnFarmerDied;
        _dog.Died -= OnDogDied;
    }

    private void Start()
    {
        _restartManager = new RestartManager();
    }

    private void OnStartButtonClicked()
    {
        _navMeshSurface2d.BuildNavMesh();

        var newPersons = _levelStarter.CreateObjects();

        foreach (var person in newPersons)
        {
            if (person is Player player)
            {
                _player = player;
            }
            else if(person is Farmer farmer)
            {
                _farmer = farmer;
            }
            else if(person is Dog dog)
            {
                _dog = dog;
            }

            _restartManager.Register(person);
        }

        _restartManager.Register(_userInput);
        _restartManager.Register(_gameUI);
        _restartManager.Register(_farmerHealthBar);

        _player.Died += OnPlayerDied;
        _farmer.Died += OnFarmerDied;
        _dog.Died += OnDogDied;

        _userInput.Enable(true);
        _gameUI.OpenGamePanel();

        List<Collider2D> colliders = new List<Collider2D>();

        foreach (var person in newPersons)
        {
            colliders.Add(person.GetComponent<Collider2D>());
        }

        IgnoreCollisions ignoreCollisions = new IgnoreCollisions(colliders);
        ignoreCollisions.MakeIgnore();
    }

    private void OnRestartButtonClicked()
    {
        _restartManager.Restart();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnPlayerDied()
    {
        _gameUI.OpenLosePanel();
        SetEndSettings();
    }

    private void OnFarmerDied()
    {
        _gameUI.OpenWinPanel();
        SetEndSettings();
    }

    private void OnDogDied()
    {
        StartCoroutine(RespawnDogCoroutine());
    }

    private IEnumerator RespawnDogCoroutine()
    {
        yield return new WaitForSeconds(_dog.RespawnTime);

        _dog.Respawn();
    }

    private void SetEndSettings()
    {
        _userInput.Enable(false);

        _player.Died -= OnPlayerDied;
        _farmer.Died -= OnFarmerDied;
    }
}
