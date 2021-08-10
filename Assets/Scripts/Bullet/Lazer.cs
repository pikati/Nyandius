using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Bullet
{

    protected override void Move()
    {
        transform.position += new Vector3(_speed + Time.deltaTime, 0, 0);
    }
}
