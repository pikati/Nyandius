using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectCollider : Collider
{
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 max;
    [SerializeField]
    private Vector2 min;
    public Vector2 Center => center;
    public Vector2 Max => max;
    public Vector2 Min => min;

    protected override void Initialize()
    {
        CType = ColliderType.Rect;
        base.Initialize();
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    public Behaviour GetBehaviour()
    {
        return behaviour;
    }
}
