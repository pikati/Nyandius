using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    protected override void Initialize()
    {
        _damage = 1 + Singleton<GameManager>.Instance.LoopNum * 3.0f;
    }

    protected override void Move()
    {
        transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
    }

    public override void OnCollision(Collider col)
    {
        if(col.CompareTag("Enemy") || col.CompareTag("Ground") || col.CompareTag("HardEnemy"))
        {
            DestroyThis();
        }
    }
}
