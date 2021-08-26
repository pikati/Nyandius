using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private ICharacterAttack _characterAttack;
    private BulletType _bulletType = BulletType.Normal;
    private SpriteRenderer _spriteRenderer;
    private bool _isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        _characterAttack = GetComponent<ICharacterAttack>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetBulletType(BulletType bulletType)
    {
        _bulletType = bulletType;
    }

    public void ActivateOption()
    {
        _isActive = true;
        _spriteRenderer.enabled = true;
    }

    public void Attack(bool isMissile)
    {
        if (!_isActive) return;
        if(isMissile)
        {
            _characterAttack.Attack(transform.position, BulletType.Missile);
        }
        else if(_bulletType == BulletType.Double)
        {
            _characterAttack.Attack(transform.position, BulletType.Normal);
        }
        else
        {
            _characterAttack.Attack(transform.position, _bulletType);
        }
    }

    public void Reset()
    {
        _isActive = false;
        _spriteRenderer.enabled = false;
    }
}
