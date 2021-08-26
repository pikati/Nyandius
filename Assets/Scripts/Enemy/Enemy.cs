using System;
using UniRx;
using UnityEngine;

public class Enemy : Character
{
    protected int _score;
    protected GameObject _bullet;
    protected SpriteRenderer _spriteRenderer;
    private GameObject _item;
    private GameObject _effect;
    private bool haveItem = false;
    protected override void Initialize()
    {
        _hp.Where(x => x <= 0)
            .Take(1)
            .Subscribe(_ => OnDead())
            .AddTo(this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var e = Singleton<EnemyResourceManager>.Instance;
        _bullet = e.BulletObj;
        _item = e.ItemObj;
        _effect = e.EffectObj;
    }

    protected override void UpdateFrame()
    {
        base.UpdateFrame();
        if (Singleton<GameManager>.Instance.IsPlayerDead())
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

    protected override void OnDead()
    {
        Singleton<CriSoundManager>.Instance.PlaySE(CueID.EnemyDead);
        Singleton<ScoreManager>.Instance.AddScore(_score);
        if(haveItem)
        {
            Instantiate(_item, transform.position, Quaternion.identity);
        }
        Instantiate(_effect, transform.position, Quaternion.identity);
        DestroyThis();
    }

    protected bool IsOffScreen()
    {
        return !_spriteRenderer.isVisible;
    }

    public void SetItem()
    {
        haveItem = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
