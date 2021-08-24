using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Bullet
{
    protected override void Initialize()
    {
        _damage = 0.04f + Singleton<GameManager>.Instance.LoopNum * 0.02f;
    }
    protected override void Move()
    {
        transform.position += new Vector3(_speed + Time.deltaTime, 0, 0);
    }

    public override void OnCollision(Collider col)
    {
        if(col.CompareTag("HardEnemy"))
        {
            DestroyThis();
        }
    }
}
