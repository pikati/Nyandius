using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : Behaviour
{
    protected Bullet()
    {

    }
    protected float _speed = 0.1f;
    protected bool _canPierce = false;
    protected override void Initialize()
    {

    }

    protected override void UpdateFrame()
    {

    }

    void OnBecameInvisible()
    {
        DestroyThis();
    }

    protected abstract void Move();
}
