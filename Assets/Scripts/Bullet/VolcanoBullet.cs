using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class VolcanoBullet : Enemy
{
    private Vector3 _direction;
    private float _gravity = -2.0f;
    public void SetDirection(Vector3 direction)
    {
        _score = 100 + (Singleton<GameManager>.Instance.LoopNum * 50);
        _hp.Value = 1 + (Singleton<GameManager>.Instance.LoopNum * 5);
        _direction = direction;
    }

    protected override void UpdateFrame()
    {
        base.UpdateFrame();
        transform.position += _direction * Time.deltaTime * 3.0f;
        _direction.y += _gravity * Time.deltaTime;
        if(IsOffScreen())
        {
            DestroyThis();
        }
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            OnDamage(col.GetComponent<Bullet>().Damage());
        }
    }

    public void SetReverse()
    {
        _gravity = 2.0f;
        _direction.y *= -1;
    }

    protected override void OnDead()
    {
        Singleton<ScoreManager>.Instance.AddScore(_score);
        DestroyThis();
    }
}
