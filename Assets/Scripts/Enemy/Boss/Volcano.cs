using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : Enemy
{
    private GameTimer _shotTimer;
    private GameManager _gameManager;
    private float _coolTimeMin;
    private float _coolTimeMax;

    protected override void Initialize()
    {
        _gameManager = Singleton<GameManager>.Instance;
        _hp.Value = 400;
        _score = 100000 * _gameManager.LoopNum * 2;
        _bullet = Resources.Load("Bullet/VolcanoBullet") as GameObject;
        _shotTimer = new GameTimer(Random.Range(0f, 2f));
        _coolTimeMin = 0.5f / (1 + _gameManager.LoopNum * 1.5f);
        _coolTimeMax = 1.0f / (1 + _gameManager.LoopNum * 1.5f);
    }

    protected override void UpdateFrame()
    {
        
        if(_shotTimer.UpdateTimer())
        {
            var b = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<VolcanoBullet>();
            b.SetDirection(new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(2.0f, 3.5f), 0));
            if(transform.rotation.z != 0)
            {
                b.SetReverse();
            }
            _shotTimer.ResetTimer(Random.Range(_coolTimeMin, _coolTimeMax));
        }
        if(_gameManager.BossTimer.IsTimeUp)
        {
            transform.position += Vector3.left * 1.5f * Time.deltaTime;
        }
        base.UpdateFrame();
        if (transform.position.x < -10.0f)
        {
            DestroyThis();
        }
    }
}
