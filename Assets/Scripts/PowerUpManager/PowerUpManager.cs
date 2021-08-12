using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    private int _powerNum = 0;
    private readonly int _maxPower = 6;
    private Speeder _speeder;
    private Missiler _missiler;
    private Barrier _barrier;
    private Mike _mike;
    private PowerUpModel _powerUpModel;
    public void Update()
    {
        if(Singleton<InputController>.Instance.Y)
        {
            PowerUp();
        }
        if(Singleton<InputController>.Instance.X)
        {
            GetPowerUp();
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

    public void SetBarrier(Barrier barrier)
    {
        _barrier = barrier;
    }

    public void SetPlayer(Mike mike)
    {
        _mike = mike;
    }

    public void SetPowerUpModel(PowerUpModel p)
    {
        _powerUpModel = p;
    }

    public void GetPowerUp()
    {
        _powerNum++;
        if(_powerNum > _maxPower)
        {
            _powerNum = 1;
        }
        _powerUpModel.SetPowerUpNum(_powerNum);
    }

    private void PowerUp()
    {
        switch (_powerNum)
        {
            case 1:
                _speeder.SpeedUp();
                _powerNum = 0;
                break;
            case 2:
                _missiler.ActiveMissiler = true;
                _powerNum = 0;
                break;
            case 3:
                _mike.ChangeBulletType(BulletType.Double);
                _mike.SetOptionBulletType(BulletType.Double);
                _powerNum = 0;
                break;
            case 4:
                _mike.ChangeBulletType(BulletType.Lazer);
                _mike.SetOptionBulletType(BulletType.Lazer);
                _powerNum = 0;
                break;
            case 5:
                _mike.ActivateOption();
                _powerNum = 0;
                break;
            case 6:
                _barrier.ActivateBarrier();
                _powerNum = 0;
                break;
            default:
                break;
        }
        _powerUpModel.SetPowerUpNum(_powerNum);
    }
}
