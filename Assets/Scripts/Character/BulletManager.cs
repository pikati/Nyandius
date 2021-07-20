using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Normal,
    Lazer,
    Missile,
    End
};

public class BulletManager : Singleton<BulletManager>
{
    

    private List<GameObject> bullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        bullets.Add(Resources.Load("Bullet/BulletNormal") as GameObject);
    }

    public void CreateBullet(BulletType type, Vector3 pos)
    {
        Instantiate(bullets[(int)type], pos, Quaternion.identity);
    }
}
