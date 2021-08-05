using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Circle,
    Rect
};

public class Collider : Behaviour
{
    
    static int n = 0;
    protected Behaviour _behaviour;
    ColliderManager _colliderManager;
    public int ID { get; private set; } = 0;
    public ColliderType CType { get; protected set; } = ColliderType.Circle;


    private void Start()
    {
        Initialize();
        ID = n++;
        _colliderManager = GameObject.Find("GameManager").GetComponent<GameManager>().GetColliderManager();
    }

    private void Update()
    {
        UpdateFrame();

    }
    protected virtual void Initialize()
    {
        _behaviour = GetComponent<Behaviour>();
    }

    protected virtual void UpdateFrame()
    {

    }

    public void DeleteCollider()
    {
        _colliderManager.DeleteCollider(this);
    }
}
