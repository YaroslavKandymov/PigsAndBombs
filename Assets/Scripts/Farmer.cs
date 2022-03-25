using System;
using System.Collections;
using UnityEngine;

public class Farmer : Person
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _castDistance;
    [SerializeField] private float _destinationDistance;
    [SerializeField] private ContactFilter2D _contactFilter2D;
    [SerializeField] private FarmerText _farmerText;

    private readonly RaycastHit2D[] _result = new RaycastHit2D[1];
    private Dog _dog;
    private int _collisionCount;
    private Coroutine _coroutine;

    public event Action PlayerFounded;

    private void OnDisable()
    {
        _dog.PlayerDetected -= OnPlayerDetected;
    }

    private void Update()
    {
        RotateSprite(Agent);
    }

    public void Init(Dog dog)
    {
        _dog = dog;
        _dog.PlayerDetected += OnPlayerDetected;
    }

    protected override void RestartBehaviour()
    {
        Agent.isStopped = true;
        _collisionCount = 0;
        _coroutine = null;
    }

    private void OnPlayerDetected()
    {
        Agent.isStopped = false;
        var destinationPoint = _dog.transform.position;
        Agent.SetDestination(destinationPoint);

        _coroutine = StartCoroutine(TryFoundPlayer(Direction));

        if ((transform.position - destinationPoint).sqrMagnitude <=
            _destinationDistance * _destinationDistance)
        {
            Agent.isStopped = true;
            StopCoroutine(_coroutine);

            if (_collisionCount <= 0)
            {
                _farmerText.Show();
                _dog.StartPatrol();
            }
            else
            {
                PlayerFounded?.Invoke();
            }
        }
    }

    private IEnumerator TryFoundPlayer(Directions direction)
    {
        while (true)
        {
            switch (direction)
            {
                case Directions.Right:
                    _collisionCount = _rigidbody2D.Cast(Vector2.right, _contactFilter2D, _result, _castDistance);
                    break;
                case Directions.Left:
                    _collisionCount = _rigidbody2D.Cast(Vector2.left, _contactFilter2D, _result, _castDistance);
                    break;
                case Directions.Up:
                    _collisionCount = _rigidbody2D.Cast(Vector2.up, _contactFilter2D, _result, _castDistance);
                    break;
                case Directions.Down:
                    _collisionCount = _rigidbody2D.Cast(Vector2.down, _contactFilter2D, _result, _castDistance);
                    break;
            }

            yield return null;
        }

    }
}
