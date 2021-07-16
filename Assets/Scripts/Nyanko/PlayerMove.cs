using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var move = Singleton<InputController>.Instance.MoveValue;
        transform.position += new Vector3(move.x * Time.deltaTime, move.y * Time.deltaTime);
        if (Singleton<InputController>.Instance.Up)
        {
            transform.position += new Vector3(0, 1.0f * Time.deltaTime, 0);
        }
        if (Singleton<InputController>.Instance.Down)
        {
            transform.position += new Vector3(0, -1.0f * Time.deltaTime, 0);

        }
        if (Singleton<InputController>.Instance.Left)
        {
            transform.position += new Vector3(-1.0f * Time.deltaTime, 0, 0);

        }
        if (Singleton<InputController>.Instance.Right)
        {
            transform.position += new Vector3(1.0f * Time.deltaTime, 0, 0);
        }
    }
}
