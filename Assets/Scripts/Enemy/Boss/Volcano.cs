using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : Enemy
{
    private GameTimer _shotTimer;
    protected override void Initialize()
    {
        _hp.Value = 500;
        _score = 10000;
        _bullet = Resources.Load("Bullet/VolcanoBullet") as GameObject;
        _shotTimer = new GameTimer(Random.Range(0f, 2f));
    }

    protected override void UpdateFrame()
    {
        if(_shotTimer.UpdateTimer())
        {
            Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<VolcanoBullet>().SetDirection(new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(2.0f,3.5f), 0));
            _shotTimer.ResetTimer(Random.Range(0.5f, 1.0f));
        }
        transform.position += Vector3.left * 1.5f * Time.deltaTime;
    }
}
