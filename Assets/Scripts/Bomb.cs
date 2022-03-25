using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionTime;
    [SerializeField] private int _damage;
    [SerializeField] private CapsuleCollider2D _collider;

    private void Awake()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.GetDamage(_damage);
        }
    }

    public void Activate()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(_explosionTime);

        _collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);

        _collider.enabled = false;
    }
}
