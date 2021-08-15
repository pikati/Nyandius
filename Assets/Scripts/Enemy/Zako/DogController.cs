using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private GameObject _dog;
    // Start is called before the first frame update
    void Start()
    {
        _dog = Resources.Load("Enemy/Dog") as GameObject;
    }

    public void CreateDog(float height)
    {
        if(height > 0)
        {
            height = 4.5f;
        }
        else
        {
            height = -5.0f;
        }
        Instantiate(_dog, new Vector3(11.0f, height, 0), Quaternion.identity);
    }
}
