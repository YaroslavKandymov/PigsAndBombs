using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
   
    private Vector3 _movePosition;

    public void Move(Vector3 touchPosition)
    {
        _movePosition = new Vector3(touchPosition.x, touchPosition.y, 0);
        _agent.SetDestination(_movePosition);
    }
}
