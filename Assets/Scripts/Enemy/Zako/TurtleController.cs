using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    private GameObject _turtle;
    // Start is called before the first frame update
    void Start()
    {
        _turtle = Resources.Load("Enemy/Turtle") as GameObject;
    }

    public void CreateTurtle(float height)
    {
        if (height > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
            height = 4.5f;
        }
        else
        {
            height = -4.8f;
        }
        Instantiate(_turtle, new Vector3(11.0f, height, 0), Quaternion.identity);
    }
}
