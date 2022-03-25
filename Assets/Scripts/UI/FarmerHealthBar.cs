using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FarmerHealthBar : MonoBehaviour, IRestartable
{
    [SerializeField] private Image _image;
    [SerializeField] private float _duration;

    private Farmer _farmer;

    public void Init(Farmer farmer)
    {
        _farmer = farmer;

        _farmer.DamageTaken += OnDamageTaken;
    }

    public void Restart()
    {
        _image.fillAmount = 1;
    }

    private void OnDamageTaken(float currentHealth, float maxHealth)
    {
        _image.DOFillAmount(currentHealth / maxHealth, _duration);

        if(_image.fillAmount <= 0)
            _farmer.DamageTaken -= OnDamageTaken;
    }
}
