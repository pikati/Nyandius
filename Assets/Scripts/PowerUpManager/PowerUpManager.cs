using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    private int _powerNum = 0;
    private readonly int _maxPower = 6;
    private Speeder _speeder;
    private Missiler _missiler;
    private Doubler _doubler = new Doubler();
    private Mike _mike;
    public void Update()
    {
        if(Singleton<InputController>.Instance.Y)
        {
            _speeder.SpeedUp();
            _missiler.ValidMissiler = true;
            _doubler.ActivateDoubler();
            _mike.IsActiveDoubler = true;
        }
    }

    public void Destory()
    {

    }

    public void SetSpeeder(Speeder speeder)
    {
        _speeder = speeder;
    }

    public void SetMissiler(Missiler missiler)
    {
        _missiler = missiler;
    }

    public Doubler GetDoubler()
    {
        return _doubler;
    }

    public void SetPlayer(Mike mike)
    {
        _mike = mike;
    }
}
