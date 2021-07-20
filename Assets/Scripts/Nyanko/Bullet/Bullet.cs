using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : Behaviour
{
    protected Bullet()
    {

    }
    protected float speed = 0.1f;
    protected bool isPlayer = true;
    protected bool canPierce = false;
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
