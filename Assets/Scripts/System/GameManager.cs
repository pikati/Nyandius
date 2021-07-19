using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ColliderManager colliderManager;
    // Start is called before the first frame update
    void Start()
    {
        colliderManager = new ColliderManager();
    }

    // Update is called once per frame
    void Update()
    {
        colliderManager.UpdateCollision();
    }

    public void RegisterCollider(CircleCollider c)
    {
        colliderManager.RegisterCollider(c);
    }

    public void RegisterCollider(RectCollider r)
    {
        colliderManager.RegisterCollider(r);
    }

    public ColliderManager GetColliderManager()
    {
        return colliderManager;
    }
}
