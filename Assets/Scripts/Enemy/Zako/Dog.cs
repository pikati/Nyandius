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
    private float _speed = 4.0f;
    private float _rad = 1.0f;
    private float _angle = 0;
    private Transform _playerTransform;
    private readonly float _attackInterval = 1.5f;
    private GameTimer _attackTimer;

    protected override void Initialize()
    {
        _score = 400;
        _hp.Value = 1;
        _attackTimer = new GameTimer(_attackInterval);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
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
                bullet.SetDirection((_playerTransform.position - transform.position).normalized);
                bullet.SetSpeed(5.0f);
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
