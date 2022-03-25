using UnityEngine;

public class ScreenSizer : MonoBehaviour
{
    private void Awake()
    {
        var width = Screen.width;
        var height = Screen.height;

        transform.localScale = new Vector3(width, height);
    }
}
