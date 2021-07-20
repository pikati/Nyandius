using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : Collider
{
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float radius;
    public Vector2 Center => center;
    public float Radius => radius;
    protected override void Initialize()
    {
        CType = ColliderType.Circle;
        base.Initialize();
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    public Behaviour GetBehaviour()
    {
        return behaviour;
    }
}



