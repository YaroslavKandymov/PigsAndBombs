using UnityEngine;

public class PlayerBombs : ObjectPool<Bomb>
{
    [SerializeField] private Bomb _bombTemplate;

    private void Awake()
    {
        Initialize(_bombTemplate);
    }

    public void PutBomb(Vector3 position)
    {
        if (TryGetObject(out Bomb bomb))
        {
            bomb.transform.position = position;
            bomb.gameObject.SetActive(true);
            bomb.Activate();
        }
    }
}
