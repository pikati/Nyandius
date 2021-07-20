using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    public BulletType type = BulletType.Normal;
    public void ShotBullet(Vector3 pos)
    {
        Singleton<BulletManager>.Instance.CreateBullet(type, pos);
    }

    public void ChangeBulletType(BulletType type)
    {
        this.type = type;
    }
}
