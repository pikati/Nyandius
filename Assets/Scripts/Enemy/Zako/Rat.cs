using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Character
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void UpdateFrame()
    {
        base.UpdateFrame();
    }

    public override void OnCollision(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("p");
        }
    }
}
