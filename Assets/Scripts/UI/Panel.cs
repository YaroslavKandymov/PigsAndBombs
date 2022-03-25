using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Open()
    {
        _canvasGroup.Open();
    }

    public void Close()
    {
        _canvasGroup.Close();
    }
}
