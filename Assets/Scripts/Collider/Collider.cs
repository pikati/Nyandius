using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Collider :MonoBehaviour
{
    protected Character character;
    ColliderManager colliderManager;

    private void Start()
    {
        Initialize();
        colliderManager = GameObject.Find("GameManager").GetComponent<GameManager>().GetColliderManager();
    }

    private void Update()
    {
        UpdateFrame();
        colliderManager.OnCollision.Subscribe(col =>
        {
            character.OnCollision(col);
        });

    }
    protected virtual void Initialize()
    {
        character = GetComponent<Character>();
    }

    protected virtual void UpdateFrame()
    {

    }
}
