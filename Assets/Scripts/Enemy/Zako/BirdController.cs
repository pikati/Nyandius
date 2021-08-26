using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private GameObject _bird;
    // Start is called before the first frame update
    void Start()
    {
        _bird = Resources.Load("Enemy/Bird") as GameObject;
    }

    public void CreateBird(float height)
    {
        Instantiate(_bird, new Vector3(11.0f, height, 0), Quaternion.identity);
        Instantiate(_bird, new Vector3(13.0f, height + 2.0f, 0), Quaternion.identity);
        Instantiate(_bird, new Vector3(13.0f, height - 2.0f, 0), Quaternion.identity);
        Instantiate(_bird, new Vector3(15.0f, height + 4.0f, 0), Quaternion.identity);
        Instantiate(_bird, new Vector3(15.0f, height, 0), Quaternion.identity).GetComponent<Enemy>()?.SetItem();
        Instantiate(_bird, new Vector3(15.0f, height - 4.0f, 0), Quaternion.identity);
    }
}
