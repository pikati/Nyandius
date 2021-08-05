using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    public void ShotBullet(Vector3 pos, BulletType type)
    {
        Singleton<BulletManager>.Instance.CreateBullet(type, pos);
    }
}
