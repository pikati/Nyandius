using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Collider myCollider;
    private void Start()
    {
        myCollider = GetComponent<Collider>();
        Initialize();
    }

    private void Update()
    {
        UpdateFrame();
    }
    protected virtual void Initialize()
    {

    }

    protected virtual void UpdateFrame()
    {

    }

    public virtual void OnCollision(Collider col)
    {

    }
}
