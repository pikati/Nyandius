using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Missiler : Singleton<Missiler>
{
    public int MissleNum { get; set; } = 0;
    public int ShooterNum { get; set; } = 0;
    public bool ActiveMissiler { get; set; } = false;
    public bool CanShotMissile
    {
        get
        {
            return ActiveMissiler && (MissleNum < ShooterNum);
        }
    }


    private void Start()
    {
        Singleton<GameManager>.Instance.PowerUpManager.SetMissiler(this);
    }
}
