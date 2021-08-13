using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private GameObject _rat;
    // Start is called before the first frame update
    void Start()
    {
        _rat = Resources.Load("Enemy/Rat") as GameObject;
    }

    public void CreateRat(float height)
    {
        Instantiate(_rat, new Vector3(11.0f, height, 0), Quaternion.identity);
        Instantiate(_rat, new Vector3(13.0f, height, 0), Quaternion.identity);
        Instantiate(_rat, new Vector3(15.0f, height, 0), Quaternion.identity);
        Instantiate(_rat, new Vector3(17.0f, height, 0), Quaternion.identity);
    }
}
