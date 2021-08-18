 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, ICharacterAttack
{
    private Shooter _shooter = new Shooter();
    private IPlayerInputEventProvider _playerInputEventProvider;
    // Start is called before the first frame update
    void Start()
    {
        _playerInputEventProvider = GetComponent<IPlayerInputEventProvider>();
    }

    public void Attack(Vector3 position, BulletType type)
    {
        if(type == BulletType.Lazer)
        {
            Singleton<CriSoundManager>.Instance.PlaySound(CueID.Lazer);
        }
        else
        {
            Singleton<CriSoundManager>.Instance.PlaySound(CueID.Shot);
        }
        _shooter.ShotBullet(position, type);
    }
}
