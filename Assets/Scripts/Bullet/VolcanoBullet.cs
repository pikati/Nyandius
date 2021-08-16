using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class VolcanoBullet : Enemy
{
    private Vector3 _direction;
    private bool _isReverse = false;
    private float _gravity = -2.0f;
    public void SetDirection(Vector3 direction)
    {
        _score = 100;
        _direction = direction;
    }

    protected override void UpdateFrame()
    {
        transform.position += _direction * Time.deltaTime * 3.0f;
        _direction.y += _gravity * Time.deltaTime;
        if(IsOffScreen())
        {
            DestroyThis();
        }
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Bullet"))
        {
            DestroyThis();
        }
    }

    public void SetReverse()
    {
        _isReverse = true;
        _gravity = 2.0f;
        _direction.y *= -1;
    }
}
