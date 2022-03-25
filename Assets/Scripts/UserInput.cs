using System;
using UnityEngine;

public class UserInput : MonoBehaviour, IRestartable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _doubleClickTime = 0.2f;

    private PlayerMover _playerMover;
    private PlayerBombs _playerBombs;
    
    private float _lastClickTime;
    private Touch _touch;
    private bool _isEnable;

    private void Update()
    {
        if(_isEnable == false)
            return;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                float timeSinceLastClick = Time.time - _lastClickTime;
                _touch = Input.GetTouch(0);

                if (timeSinceLastClick <= _doubleClickTime)
                {
                    _playerBombs.PutBomb(_playerMover.transform.position);
                }
                else
                {
                    var touchPosition = _camera.ScreenToWorldPoint(_touch.position);

                    _playerMover.Move(touchPosition);
                }

                _lastClickTime = Time.time;
            }
        }
    }

    public void Init(PlayerMover playerMover, PlayerBombs playerBombs)
    {
        if(playerMover == null)
            throw new NullReferenceException();

        if (playerBombs == null)
            throw new NullReferenceException();

        _playerMover = playerMover;
        _playerBombs = playerBombs;
    }

    public void Enable(bool value)
    {
        _isEnable = value;
    }

    public void Restart()
    {
        Enable(true);
    }
}
