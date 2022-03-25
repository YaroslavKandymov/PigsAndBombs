using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Person : MonoBehaviour, IDamageable, IRestartable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _down;
    [SerializeField] private Vector2 _startPosition;

    private Directions _direction;

    public event Action<float, float> DamageTaken;
    public event Action Died;

    public float Health { get; private set; }
    public NavMeshAgent Agent => _agent;
    public Vector2 StartPosition => _startPosition;
    public Directions Direction => _direction;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private void Awake()
    {
        Health = _maxHealth;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void OnValidate()
    {
        if (_maxHealth < 0)
            _maxHealth = 0;
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        DamageTaken?.Invoke(Health, _maxHealth);

        if (Health <= 0)
        {
            Died?.Invoke();
            Die();
        }
    }

    public void Restart()
    {
        RestartPosition();
        RestartBehaviour();

        gameObject.SetActive(true);
    }

    protected abstract void RestartBehaviour();

    protected virtual void Die()
    {
        Health = _maxHealth;
        gameObject.SetActive(false);
    }

    protected void RestartPosition()
    {
        transform.position = _startPosition;
    }

    protected void RotateSprite(NavMeshAgent agent)
    {
        if (agent.velocity.x > 0.5f)
        {
            _spriteRenderer.sprite = _right;
            _direction = Directions.Right;
        }
        else if (agent.velocity.x < -0.5f)
        {
            _spriteRenderer.sprite = _left;
            _direction = Directions.Left;
        }
        else if (agent.destination.y > 0.5f)
        {
            _spriteRenderer.sprite = _up;
            _direction = Directions.Up;
        }
        else if (agent.destination.y < -0.5f)
        {
            _spriteRenderer.sprite = _down;
            _direction = Directions.Down;
        }
    }
}
