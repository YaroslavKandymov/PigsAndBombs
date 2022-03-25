using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Dog _dogTemplate;
    [SerializeField] private Farmer _farmerTemplate;
    [SerializeField] private PlayerBombs _playerBombs;
    [SerializeField] private FarmerHealthBar _farmerHealthBar;
    [SerializeField] private Map _map;

    private Player _player;
    private Dog _dog;
    private Farmer _farmer;

    public IEnumerable<Person> CreateObjects()
    {
        List<Person> persons = new List<Person>();

        _player = Create(_playerTemplate, _playerTemplate.StartPosition) as Player;
        _dog = Create(_dogTemplate, _dogTemplate.StartPosition) as Dog;
        _farmer = Create(_farmerTemplate, _farmerTemplate.StartPosition) as Farmer;

        persons.Add(_player);
        persons.Add(_dog);
        persons.Add(_farmer);

        var playerMover = _player.GetComponent<PlayerMover>();
        var dogMover = _dog.GetComponent<DogMover>();

        _userInput.Init(playerMover, _playerBombs);
        _farmerHealthBar.Init(_farmer);
        dogMover.Init(_map);
        _player.Init(_farmer);
        _farmer.Init(_dog);

        return persons;
    }

    private Person Create(Person person, Vector3 position)
    {
        return Instantiate(person, position, Quaternion.identity);
    }
}
