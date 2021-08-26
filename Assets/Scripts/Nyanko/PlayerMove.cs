using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private IPlayerInputEventProvider _playerInputEventProvider;
    private Speeder _speeder = new Speeder();
    Vector3 _screenLeftBottom;
    Vector3 _screenRightTop;
    void Start()
    {
        _playerInputEventProvider = GetComponent<IPlayerInputEventProvider>();
        _screenLeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        _speeder.Initialize();
    }

    void Update()
    {
        Move(_playerInputEventProvider.MoveDirection.Value);
    }

    private void Move(Vector2 move)
    {
        var pos = transform.position;
        if(pos.x < _screenLeftBottom.x)
        {
            if(move.x < 0)
            {
                move.x = 0;
            }
        }
        else if(pos.x > _screenRightTop.x)
        {
            if(move.x > 0)
            {
                move.x = 0;
            }
        }
        if(pos.y < _screenLeftBottom.y)
        {
            if(move.y < 0)
            {
                move.y = 0;
            }
        }
        else if(pos.y > _screenRightTop.y)
        {
            if(move.y > 0)
            {
                move.y = 0;
            }
        }
        transform.position += new Vector3(move.x, move.y) * (_speed + _speeder.SpeedUpNum) * Time.deltaTime;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(-5, 0, 0);
    }
}
