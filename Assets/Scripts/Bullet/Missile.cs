using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    private Missiler _missiler;
    private Vector2 _direction = new Vector2(1.0f, -1.0f);
    private bool _isCollisionGround = false;

    protected override void Initialize()
    {
        _missiler = Singleton<Missiler>.Instance;
        _missiler.MissleNum++;
        _speed = 0.01f;
    }

    protected override void Move()
    {
        if (!_isCollisionGround)
        {
            _direction = new Vector2(1.0f, -1.0f);
        }
        transform.position += new Vector3(_speed * _direction.x, _speed * _direction.y, 0);
        _isCollisionGround = false;
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Ground"))
        {
            _isCollisionGround = true;
            _direction = new Vector2(1.0f, 0);
            //進行方向横
        }
        else if (col.CompareTag("Enemy"))
        {
            //ダメージ処理
            DestroyThis();
        }
    }

    protected override void OnBecameInvisible()
    {
        DestroyMissle();
        base.OnBecameInvisible();
    }

    private void DestroyMissle()
    {
        _missiler.MissleNum--;

    }
}
