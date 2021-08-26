using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Dog : Enemy
{
    private enum DogTask
    {
        Attack
    }
    TaskList<DogTask> _task = new TaskList<DogTask>();
    private Transform _playerTransform;
    private readonly float _attackInterval = 1.5f;
    private GameTimer _attackTimer;
    private GameManager _gm;

    protected override void Initialize()
    {
        _gm = Singleton<GameManager>.Instance;
        _score = 400 + (_gm.LoopNum * 200);
        _hp.Value = 1 + (_gm.LoopNum * 8);
        _attackTimer = new GameTimer(_attackInterval);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask())
            .AddTo(this);
        base.Initialize();
    }

    private void OnTaskAttackEnter()
    {

    }

    private bool OnTaskAttackUpdate()
    {
        var pos = transform.position;
        if(pos.x < 10.0f)
        {
            if(_attackTimer.UpdateTimer())
            {
                var bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                var dir  = (_playerTransform.position - transform.position).normalized;
                bullet.SetDirection(dir);
                bullet.SetSpeed(5.0f);
                if(_gm.LoopNum >= 1)
                {
                    var bullet2 = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                    bullet2.SetDirection(dir);
                    bullet2.SetSpeed(3.0f);
                }
                if(_gm.LoopNum >= 2)
                {
                    var bullet2 = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                    bullet2.SetDirection(dir);
                    bullet2.SetSpeed(7.0f);
                }
                _attackTimer.ResetTimer();
            }
        }
        transform.position += Vector3.left * 1.5f * Time.deltaTime;
        return IsOffScreen() && pos.x < -10.0f;
    }

    private void OnTaskAttackExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(DogTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);
    }

    private void SetTask()
    {
        _task.AddTask(DogTask.Attack);
    }
}
