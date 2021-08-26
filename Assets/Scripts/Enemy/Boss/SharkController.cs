using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    private GameObject _shark;
    // Start is called before the first frame update
    void Start()
    {
        _shark = Resources.Load("Enemy/Shark") as GameObject;
    }

    public void CreateShark(float height)
    {
        Instantiate(_shark, new Vector3(11.0f, height, 0), Quaternion.identity);
    }
}
