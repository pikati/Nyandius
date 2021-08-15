using System;
using UniRx;
using UnityEngine;

public class Enemy : Character
{
    protected int _score;
    protected GameObject _bullet;
    protected SpriteRenderer _spriteRenderer;
    private GameObject _item;
    private bool haveItem = false;
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
            OnDamage(1);
        }
    }

    protected override void OnDead()
    {
        Singleton<ScoreManager>.Instance.AddScore(_score);
        if(haveItem)
        {
            Instantiate(_item, transform.position, Quaternion.identity);
        }
        DestroyThis();
    }

    protected bool IsOffScreen()
    {
        return !_spriteRenderer.isVisible;
    }

    public void SetItem()
    {
        haveItem = true;
        _item = Resources.Load("Item/Churu") as GameObject;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
