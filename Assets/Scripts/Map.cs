using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform _leftUpCorner;
    [SerializeField] private Transform _rightDownCorner;

    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(_leftUpCorner.position.x, _rightDownCorner.position.x), Random.Range(_leftUpCorner.position.y, _rightDownCorner.position.y));
    }
}
