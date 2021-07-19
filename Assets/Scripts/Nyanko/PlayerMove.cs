using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    

    [SerializeField]
    private float speed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {

        var move = Singleton<InputController>.Instance.MoveValue;
        if(move.magnitude > 0)
        {
            Move(move);
        }
        else
        {
            var keyMove = new Vector2(-Singleton<InputController>.Instance.Left + Singleton<InputController>.Instance.Right, Singleton<InputController>.Instance.Up + -Singleton<InputController>.Instance.Down);
            Move(keyMove);
        }
    }

    private void Move(Vector2 move)
    {
        if(move.magnitude > 0)
        {
            transform.position += new Vector3(move.x * Time.deltaTime, move.y * Time.deltaTime) * speed;
        }
    }
}
