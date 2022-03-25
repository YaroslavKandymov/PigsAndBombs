using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DogMover : MonoBehaviour
{
    [SerializeField] private float _timeBetweenMoves;
    [SerializeField] private NavMeshAgent _agent;

    private Map _map;
    private WaitForSeconds _seconds;
    private Coroutine _coroutine;

    private void Awake()
    {
        _seconds = new WaitForSeconds(_timeBetweenMoves);
    }

    public void Init(Map map)
    {
        if(map == null)
            throw new NullReferenceException();

        _map = map;
    }

    public void Move()
    {
        if(_coroutine != null)
            return;

        _agent.isStopped = false;
        _coroutine = StartCoroutine(MoveCoroutine());
    }

    public void Stop()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _agent.isStopped = true;
        _coroutine = null;
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            _agent.SetDestination(_map.GetRandomPosition());

            yield return _seconds;
        }
    }
}
