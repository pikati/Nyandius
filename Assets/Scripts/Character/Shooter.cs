using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    public void ShotBullet(Vector3 pos, BulletType type)
    {
        if(type == BulletType.Double)
        {
            Singleton<BulletManager>.Instance.CreateBullet(BulletType.Normal, pos + new Vector3(0, 0.2f, 0));
            Singleton<BulletManager>.Instance.CreateBullet(BulletType.Normal, pos - new Vector3(0, 0.2f, 0));
            Singleton<BulletManager>.Instance.CreateBullet(BulletType.Up, pos);
        }
        else
        {
            Singleton<BulletManager>.Instance.CreateBullet(type, pos);
        }
    }
}
