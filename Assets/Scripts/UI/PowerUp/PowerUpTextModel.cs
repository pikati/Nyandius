using UniRx;
using UnityEngine;

public class PowerUpTextModel : MonoBehaviour
{
    private BoolReactiveProperty _canSpeedUp = new BoolReactiveProperty(true);
    private BoolReactiveProperty _canMissile = new BoolReactiveProperty(true);
    private BoolReactiveProperty _canDouble = new BoolReactiveProperty(true);
    private BoolReactiveProperty _canLazer = new BoolReactiveProperty(true);
    private BoolReactiveProperty _canOption = new BoolReactiveProperty(true);

    public IReadOnlyReactiveProperty<bool> CanSpeedUp => _canSpeedUp;
    public IReadOnlyReactiveProperty<bool> CanMissile => _canMissile;
    public IReadOnlyReactiveProperty<bool> CanDouble => _canDouble;
    public IReadOnlyReactiveProperty<bool> CanLazer => _canLazer;
    public IReadOnlyReactiveProperty<bool> CanOption => _canOption;
    // Start is called before the first frame update
    void Start()
    {
        _canSpeedUp.AddTo(this);
        _canMissile.AddTo(this);
        _canDouble.AddTo(this);
        _canLazer.AddTo(this);
        _canOption.AddTo(this);
    }

    public void IsSpeedUp(bool b)
    {
        _canSpeedUp.Value = b;
    }

    public void IsMissile(bool b)
    {
        _canMissile.Value = b;
    }

    public void IsDouble(bool b)
    {
        _canDouble.Value = b;
    }
    
    public void IsLazer(bool b)
    {
        _canLazer.Value = b;
    }

    public void IsOption(bool b)
    {
        _canOption.Value = b;
    }

    public void Reset()
    {
        _canSpeedUp.Value = true;
        _canMissile.Value = true;
        _canDouble.Value = true;
        _canLazer.Value = true;
        _canOption.Value = true;
    }
}
