using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Missiler : Singleton<Missiler>
{
    private Shooter _missleShooter;
    public int MissleNum { get; set; } = 0;
    public int ShooterNum { get; set; } = 0;
    public bool ValidMissiler { get; set; } = false;
    public bool CanShotMissile
    {
        get
        {
            return ValidMissiler && (MissleNum < ShooterNum);
        }
    }


    private void Start()
    {
        Singleton<GameManager>.Instance.PowerUpManager.SetMissiler(this);
    }
}
