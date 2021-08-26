using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceManager : Singleton<EnemyResourceManager>
{
    public GameObject BulletObj { get; private set; }
    public GameObject ItemObj { get; private set; }
    public GameObject EffectObj { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        BulletObj = Resources.Load("Bullet/EnemyBullet") as GameObject;
        ItemObj = Resources.Load("Item/Churu") as GameObject;
        EffectObj = Resources.Load("Effect/Explosion") as GameObject;
    }
}
