using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : Singleton<BulletManager>
{
    

    private List<GameObject> _bullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _bullets.Add(Resources.Load("Bullet/BulletNormal") as GameObject);
    }

    public void CreateBullet(BulletType type, Vector3 pos)
    {
        Instantiate(_bullets[(int)type], pos, Quaternion.identity);
    }
}
