using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{

    protected override void Initialize()
    {
        
    }
    protected override void UpdateFrame()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += new Vector3(speed + Time.deltaTime, 0, 0);
    }

    public override void OnCollision(Collider col)
    {
        if(!col.CompareTag("Player"))
        {
            DestroyThis();
        }
    }
}
