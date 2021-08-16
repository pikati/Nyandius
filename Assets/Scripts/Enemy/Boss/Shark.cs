using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Shark : Enemy
{
    private enum SharkTask
    {
        Enter,
        Attack
    }
    TaskList<SharkTask> _task = new TaskList<SharkTask>();
    private GameTimer _shotTimer;
    private GameObject _squirrel;
    private Vector3 _direction;
    private float _speed;
    private float _maxHP;

    protected override void Initialize()
    {
        _score = 2000;
        _hp.Value = 50;
        _maxHP = _hp.Value;
        _shotTimer = new GameTimer();
        _speed = 2.0f;
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
        base.Initialize();
    }

    private void OnTaskEnterEnter()
    {
        _direction = Vector3.left;
    }

    private bool OnTaskEnterUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        if(transform.position.x < 5.0f)
        {
            return true;
        }
        return false;
    }

    private void OnTaskEnterExit()
    {

    }

    private void OnTaskAttackEnter()
    {
        _direction = Vector3.up;
        _shotTimer.ResetTimer(Random.Range(0.5f, 1.0f));
    }

    private bool OnTaskAttackUpdate()
    {
        var pos = transform.position;
        if(pos.y >3)
        {
            _direction = Vector3.down;
        }
        else if(pos.y < -3)
        {
            _direction = Vector3.up;
        }
        if (_shotTimer.UpdateTimer())
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.SetDirection(Vector3.left);
            bullet.SetSpeed(6.5f);
            _shotTimer.ResetTimer(Random.Range(0.2f, 1.0f));
        }
        transform.position += _direction * _speed * 2.0f * Time.deltaTime;
        float c = _hp.Value / _maxHP;
        _spriteRenderer.color = new Color(1, c, c, 1);
        return IsOffScreen() && pos.x < -10.0f;
    }

    private void OnTaskAttackExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(SharkTask.Enter, OnTaskEnterEnter, OnTaskEnterUpdate, OnTaskEnterExit);
        _task.DefineTask(SharkTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);
    }

    private void SetTask()
    {
        _task.AddTask(SharkTask.Enter);
        _task.AddTask(SharkTask.Attack);
    }
}
