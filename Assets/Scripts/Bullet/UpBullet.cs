using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBullet : Bullet
{
    protected override void Initialize()
    {
        _damage = 1 + Singleton<GameManager>.Instance.LoopNum * 1.5f;
    }
    protected override void Move()
    {
        transform.position += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, 0);
    }

    public override void OnCollision(Collider col)
    {
        if (!col.CompareTag("Player"))
        {
            DestroyThis();
        }
    }
}
