using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private IPlayerInputEventProvider _playerInputEventProvider;
    void Start()
    {
        _playerInputEventProvider = GetComponent<IPlayerInputEventProvider>();
    }

    void Update()
    {
        Move(_playerInputEventProvider.MoveDirection.Value);
    }

    private void Move(Vector2 move)
    {
        transform.position += new Vector3(move.x * Time.deltaTime, move.y * Time.deltaTime) * _speed;
    }
}
