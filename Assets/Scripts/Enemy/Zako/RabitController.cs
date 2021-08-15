using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitController : MonoBehaviour
{
    private GameObject _rabbit;
    // Start is called before the first frame update
    void Start()
    {
        _rabbit = Resources.Load("Enemy/Rabbit") as GameObject;
    }

    public void CreateRabbit(float height)
    {
        Instantiate(_rabbit, new Vector3(11.0f, height + 0.5f, 0), Quaternion.identity);
        Instantiate(_rabbit, new Vector3(11.0f, height - 0.5f, 0), Quaternion.identity);
    }
}
