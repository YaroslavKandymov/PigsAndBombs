using System.Collections;
using UnityEngine;

public class FarmerText : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration;

    private void Awake()
    {
        _canvasGroup.Close();
    }

    public void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        _canvasGroup.Open();

        yield return new WaitForSeconds(_duration);

        _canvasGroup.Close();
    }
}
