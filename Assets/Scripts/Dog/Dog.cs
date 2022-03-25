using System;
using UnityEngine;

public class Dog : Person
{
    [SerializeField] private DogMover _mover;
    [SerializeField] private DogsFieldOfView _fieldOfView;
    [SerializeField] private Sprite _angryLeft;
    [SerializeField] private Sprite _angryRight;
    [SerializeField] private Sprite _angryUp;
    [SerializeField] private Sprite _angryDown;
    [SerializeField] private float _respawnTime;

    public event Action PlayerDetected;

    public float RespawnTime => _respawnTime;

    private void Update()
    {
        if (_fieldOfView.PlayerDetected)
        {
            _mover.Stop();
            PlayerDetected?.Invoke();
            RotateAngrySprite(Direction);
        }
        else
        {
            _mover.Move();
            RotateSprite(Agent);
            _fieldOfView.SetDirection(Direction);
        }
    }

    public void StartPatrol()
    {
        _fieldOfView.Clear();
    }

    public void Respawn()
    {
        transform.position = StartPosition;
        gameObject.SetActive(true);
    }

    protected override void RestartBehaviour()
    {
        _fieldOfView.Clear();
    }

    protected override void Die()
    {
        _fieldOfView.Clear();
    }

    private void RotateAngrySprite(Directions direction)
    {
        switch (direction)
        {
            case Directions.Right:
                SpriteRenderer.sprite = _angryRight;
                break;
            case Directions.Left:
                SpriteRenderer.sprite = _angryLeft;
                break;
            case Directions.Up:
                SpriteRenderer.sprite = _angryUp;
                break;
            case Directions.Down:
                SpriteRenderer.sprite = _angryDown;
                break;
        }
    }
}
