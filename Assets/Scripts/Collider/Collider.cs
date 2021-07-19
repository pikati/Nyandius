using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider :MonoBehaviour
{
    protected Character character;
    ColliderManager colliderManager;

    private void Start()
    {
        Initialize();
        var a = GameObject.Find("GameManager");
        var b = a.GetComponent<GameManager>();
        colliderManager = b.GetColliderManager();
    }

    private void Update()
    {
        UpdateFrame();

    }
    protected virtual void Initialize()
    {
        character = GetComponent<Character>();
    }

    protected virtual void UpdateFrame()
    {

    }
}
