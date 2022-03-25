using System.Collections.Generic;
using UnityEngine;

public class DogsFieldOfView : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _contactFilter2D;

    private Vector2 _direction;
    private Transform _transform;
    private List<RaycastHit2D> _results = new List<RaycastHit2D>();
    private int _collision;

    public bool PlayerDetected { get; private set; }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _collision = Physics2D.Raycast(_transform.position, _direction, _contactFilter2D, _results);

        PlayerDetected = _collision > 0;
    }

    public void SetDirection(Directions direction)
    {
        if (direction == Directions.Right)
            _direction = Vector2.right;
        else if (direction == Directions.Left)
            _direction = Vector2.left;
        else if (direction == Directions.Up)
            _direction = Vector2.up;
        else if (direction == Directions.Down)
            _direction = Vector2.down;
    }

    public void Clear()
    {
        _results.Clear();
        _collision = 0;
    }
}
