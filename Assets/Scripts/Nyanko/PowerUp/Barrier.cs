using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
public class Barrier : MonoBehaviour
{
    [SerializeField]
    private GameObject _barrierObject;
    private SpriteRenderer _barrierRenderer;
    private int _barrierNum = 0;
    private bool _isInvincible = false;

    private void Start()
    {
        Singleton<GameManager>.Instance.PowerUpManager.SetBarrier(this);
        _barrierRenderer = _barrierObject.GetComponent<SpriteRenderer>();
    }

    public void ActivateBarrier()
    {
        _barrierNum = 10;
        _barrierObject.SetActive(true);
        ChangeBarrierColor();
    }

    public void DamageBarrier(in int damage)
    {
        if (_isInvincible) return;
        _barrierNum -= damage;
        ChangeBarrierColor();
        if (_barrierNum <= 0)
        {
            DeactivateBarrier();
            _barrierNum = 0;
        }
    }

    public bool IsActiveBarrier()
    {
        return _barrierNum != 0;
    }

    public void DeactivateBarrier()
    {
        _barrierObject.SetActive(false);
        _barrierNum = 0;
    }

    private void ChangeBarrierColor()
    {
        float c = _barrierNum / 10.0f;
        _barrierRenderer.color = new Color(1, c, c, 1);
    }
}
