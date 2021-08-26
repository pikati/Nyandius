using UniRx;
using UnityEngine;

public class PowerUpModel : MonoBehaviour
{
    private ReactiveProperty<int> _powerUpNum = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> PowerUpNum => _powerUpNum;
    // Start is called before the first frame update

    private void Start()
    {
        _powerUpNum.AddTo(this);
    }
    public void SetPowerUpNum(int n)
    {
        _powerUpNum.Value = n;
    }
}
