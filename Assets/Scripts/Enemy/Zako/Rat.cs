using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy
{
    protected override void Initialize()
    {
        _score = 100;
        _hp.Value = 1;
        base.Initialize();
    }
}
