using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectCollider : MonoBehaviour
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

    private void Start()
    {
        Singleton<GameManager>.Instance.RegisterCollider(this);
    }

    void Update()
    {
        
    }
}
