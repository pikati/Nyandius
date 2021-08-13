using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private Vector3 _direction;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    protected override void Move()
    {
        transform.position += _direction * Time.deltaTime * _speed;
    }

    public override void OnCollision(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            DestroyThis();
        }
    }
}
