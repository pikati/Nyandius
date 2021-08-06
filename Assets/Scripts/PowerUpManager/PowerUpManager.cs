using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    private Speeder _speeder;

    public void Update()
    {
        if(Singleton<InputController>.Instance.Y)
        {
            _speeder.SpeedUp();
        }
    }

    public void Destory()
    {

    }

    public void SetSpeeder(Speeder speeder)
    {
        _speeder = speeder;
    }
}
