using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Circle,
    Rect
};

public class Collider :MonoBehaviour
{
    
    static int n = 0;
    protected Behaviour behaviour;
    ColliderManager colliderManager;
    public int ID { get; private set; } = 0;
    public ColliderType CType { get; protected set; } = ColliderType.Circle;


    private void Start()
    {
        Initialize();
        ID = n++;
        colliderManager = GameObject.Find("GameManager").GetComponent<GameManager>().GetColliderManager();
    }

    private void Update()
    {
        UpdateFrame();

    }
    protected virtual void Initialize()
    {
        behaviour = GetComponent<Behaviour>();
    }

    protected virtual void UpdateFrame()
    {

    }

    public void DeleteCollider()
    {
        colliderManager.DeleteCollider(this);
    }
}
