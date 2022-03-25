using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions
{
    private List<Collider2D> _collider2D = new List<Collider2D>();

    public IgnoreCollisions(IEnumerable<Collider2D> colliders)
    {
        foreach (var collider in colliders)
            _collider2D.Add(collider);
    }

    public void MakeIgnore()
    {
        for (int i = 0; i < _collider2D.Count; i++)
        {
            for (int j = 0; j < _collider2D.Count; j++)
            {
                Physics2D.IgnoreCollision(_collider2D[i], _collider2D[j]);
            }
        }
    }
}
