using System;
using UniRx;
using UnityEngine;

public class Enemy : Character
{
    protected int _score;
    protected override void Initialize()
    {
        _hp.Where(x => x <= 0)
            .Subscribe(_ => OnDead())
            .AddTo(this);
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
}
