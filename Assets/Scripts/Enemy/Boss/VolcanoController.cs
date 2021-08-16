using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoController : MonoBehaviour
{
    private GameObject _volcano;
    // Start is called before the first frame update
    void Start()
    {
        _volcano = Resources.Load("Enemy/Volcano") as GameObject;
    }

    public void CreateVolcano(float height)
    {
        if (height > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
            height = 3.8f;
        }
        else
        {
            height = -4.0f;
        }
        Instantiate(_volcano, new Vector3(11.0f, height, 0), Quaternion.identity);
    }
}
