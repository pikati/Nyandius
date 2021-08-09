using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PowerUpManager PowerUpManager { get; private set; }
    private ColliderManager colliderManager;

    // Start is called before the first frame update
    void Awake()
    {
        colliderManager = new ColliderManager();
        PowerUpManager = new PowerUpManager();
    }

    private void Start()
    {
        PowerUpManager.SetPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Mike>());
    }

    // Update is called once per frame
    void Update()
    {
        colliderManager.UpdateCollision();
        PowerUpManager.Update();
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

    public bool IsActiveDouble()
    {
        return PowerUpManager.GetDoubler().ValidDoubler;
    }

    private void OnDestroy()
    {
        PowerUpManager.Destory();   
    }
}
