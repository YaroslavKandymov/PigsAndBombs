public class Player : Person
{
    private Farmer _farmer;

    private void OnDestroy()
    {
        _farmer.PlayerFounded -= OnPlayerFounded;
    }

    private void Update()
    {
        RotateSprite(Agent);
    }

    public void Init(Farmer farmer)
    {
        _farmer = farmer;

        _farmer.PlayerFounded += OnPlayerFounded;
    }

    private void OnPlayerFounded()
    {
        Die();
    }

    protected override void RestartBehaviour()
    {
    }
}
