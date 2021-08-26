using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : Collider
{
    [SerializeField]
    private Vector2 _center;
    [SerializeField]
    private float _radius;
    public Vector2 Center => _center;
    public float Radius => _radius;
    protected override void Initialize()
    {
        CType = ColliderType.Circle;
        base.Initialize();
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    public Behaviour GetBehaviour()
    {
        return _behaviour;
    }
}



