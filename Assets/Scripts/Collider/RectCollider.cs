using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectCollider : Collider
{
    [SerializeField]
    private Vector2 _center;
    [SerializeField]
    private Vector2 _max;
    [SerializeField]
    private Vector2 _min;
    public Vector2 Center => _center;
    public Vector2 Max => _max;
    public Vector2 Min => _min;

    protected override void Initialize()
    {
        CType = ColliderType.Rect;
        base.Initialize();
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    public Behaviour GetBehaviour()
    {
        return _behaviour;
    }
}
