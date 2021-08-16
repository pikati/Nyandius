using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class VolcanoBullet : Enemy
{
    private Vector3 _direction;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    protected override void UpdateFrame()
    {
        transform.position += _direction * Time.deltaTime * 3.0f;
        _direction.y += -2.0f * Time.deltaTime;
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
}
