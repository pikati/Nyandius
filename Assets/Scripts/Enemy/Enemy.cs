using System;
using UniRx;
using UnityEngine;

public class Enemy : Character
{
    protected int _score;
    protected GameObject _bullet;
    protected SpriteRenderer _spriteRenderer;
    protected override void Initialize()
    {
        _hp.Where(x => x <= 0)
            .Subscribe(_ => OnDead())
            .AddTo(this);
        _bullet = Resources.Load("Bullet/EnemyBullet") as GameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void UpdateFrame()
    {
        base.UpdateFrame();
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            _hp.Value--;
        }
    }

    protected override void OnDead()
    {
        Singleton<ScoreManager>.Instance.AddScore(_score);
        DestroyThis();
    }

    protected bool IsOffScreen()
    {
        return !_spriteRenderer.isVisible;
    }
}
