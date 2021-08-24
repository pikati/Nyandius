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
    private PowerUpTextModel _model;

    private int _speedUpNum = 0;
    private int _optionNum = 0;

    public void Initialize()
    {
        _model = GameObject.Find("PowerUpModel").GetComponent<PowerUpTextModel>();
    }

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
                if (!_model.CanSpeedUp.Value) return;
                PowerUpSpeed();
                break;
            case 2:
                if (!_model.CanMissile.Value) return;
                PowerUpMissile();
                break;
            case 3:
                if (!_model.CanDouble.Value) return;
                PowerUpDouble();
                break;
            case 4:
                if (!_model.CanLazer.Value) return;
                PowerUpLazer();
                break;
            case 5:
                if (!_model.CanOption.Value) return;
                PowerUpOption();
                break;
            case 6:
                PowerUpBarrier();
                break;
            default:
                break;
        }
        _powerUpModel.SetPowerUpNum(_powerNum);
    }

    private void PowerUpSpeed()
    {
        _speeder.SpeedUp();
        _powerNum = 0;
        _speedUpNum++;
        if (_speedUpNum == 5)
        {
            _model.IsSpeedUp(false);
        }
    }

    private void PowerUpMissile()
    {
        _missiler.ActiveMissiler = true;
        _powerNum = 0;
        _model.IsMissile(false);
    }

    private void PowerUpDouble()
    {
        _mike.ChangeBulletType(BulletType.Double);
        _mike.SetOptionBulletType(BulletType.Double);
        _powerNum = 0;
        _model.IsDouble(false);
        _model.IsLazer(true);
    }

    private void PowerUpLazer()
    {
        _mike.ChangeBulletType(BulletType.Lazer);
        _mike.SetOptionBulletType(BulletType.Lazer);
        _powerNum = 0;
        _model.IsLazer(false);
        _model.IsDouble(true);
    }

    private void PowerUpOption()
    {
        _mike.ActivateOption();
        _powerNum = 0;
        _optionNum++;
        if (_optionNum == 2)
        {
            _model.IsOption(false);
        }
    }

    private void PowerUpBarrier()
    {
        _barrier.ActivateBarrier();
        _powerNum = 0;
    }

    public void Reset()
    {
        _speeder.Reset();
        _powerNum = 0;
        _powerUpModel.SetPowerUpNum(_powerNum);
        _speedUpNum = 0;
        _optionNum = 0;
        _model.Reset();
    }
}
