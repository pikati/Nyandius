using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Rat : Enemy
{
    private enum RatTask
    {
        MoveToLeft,
        Attack,
        MoveToRight
    };

    TaskList<RatTask> _task = new TaskList<RatTask>();
    private Transform _playerTransform;
    private Vector3 _moveDirection;
    private float _speed = 5.0f;
    private GameManager _gm;

    protected override void Initialize()
    {
        _gm = Singleton<GameManager>.Instance;
        _score = 200 + (_gm.LoopNum * 50);
        _hp.Value = 1 + (_gm.LoopNum * 3);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
        base.Initialize();
    }

    private void OnTaskMoveToLeftEnter()
    {
        _moveDirection = Vector3.left;
        
    }

    private bool OnTaskMoveToLeftUpdate()
    {
        transform.position += _moveDirection * Time.deltaTime * _speed;
        if (transform.position.x - _playerTransform.position.x < 4)
        {
            return true;
        }
        return false;
    }

    private void OnTaskMoveToLeftExit()
    {

    }

    private void OnTaskAttackEnter()
    {

    }

    private bool OnTaskAttackUpdate()
    {
        var bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet.SetDirection((_playerTransform.position - transform.position).normalized);
        bullet.SetSpeed(5.0f + (_gm.LoopNum * 1.0f));
        return true;
    }

    private void OnTaskAttackExit()
    {

    }

    private void OnTaskMoveToRightEnter()
    {
        if(transform.position.y < 0)
        {
            _moveDirection = new Vector3(1.0f, 0.5f, 0);
        }
        else
        {
            _moveDirection = new Vector3(1.0f, -0.5f, 0);
        }
    }

    private bool OnTaskMoveToRightUpdate()
    {
        transform.position += _moveDirection * Time.deltaTime * _speed * 0.75f;
        return IsOffScreen();
    }

    private void OnTaskMoveToRightExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(RatTask.MoveToLeft, OnTaskMoveToLeftEnter, OnTaskMoveToLeftUpdate, OnTaskMoveToLeftExit);
        _task.DefineTask(RatTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);
        _task.DefineTask(RatTask.MoveToRight, OnTaskMoveToRightEnter, OnTaskMoveToRightUpdate, OnTaskMoveToRightExit);

    }

    private void SetTask()
    {
        _task.AddTask(RatTask.MoveToLeft);
        _task.AddTask(RatTask.Attack);
        _task.AddTask(RatTask.MoveToRight);
    }
}
