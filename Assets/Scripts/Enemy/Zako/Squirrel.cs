using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Squirrel : Enemy
{
    private enum SquirelTask
    {
        Move,
        Attack
    };

    TaskList<SquirelTask> _task = new TaskList<SquirelTask>();
    private Transform _playerTransform;
    private Vector3 _moveDirection;
    private float _speed = 4.5f;

    protected override void Initialize()
    {
        _score = 300;
        _hp.Value = 1;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
        base.Initialize();
    }

    private void OnTaskMoveEnter()
    {
        var pos = transform.position;
        if (pos.y < 0)
        {
            _moveDirection = new Vector3(-1.5f / _speed, 1.0f, 0);
        }
        else
        {
            _moveDirection = new Vector3(-1.5f / _speed, -1.0f, 0);
        }
    }

    private bool OnTaskMoveUpdate()
    {
        var pPos = _playerTransform.position;
        var pos = transform.position;

        if (Mathf.Abs (pPos.y - pos.y) < 0.2f)
        {
            return true;
        }
        transform.position += _moveDirection * Time.deltaTime * _speed;
        return false;
    }

    private void OnTaskMoveExit()
    {

    }

    private void OnTaskAttackEnter()
    {
        _moveDirection = Vector3.left;
    }

    private bool OnTaskAttackUpdate()
    {
        transform.position += _moveDirection * Time.deltaTime * _speed;
        return IsOffScreen();
    }

    private void OnTaskAttackExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(SquirelTask.Move, OnTaskMoveEnter, OnTaskMoveUpdate, OnTaskMoveExit);
        _task.DefineTask(SquirelTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);

    }

    private void SetTask()
    {
        _task.AddTask(SquirelTask.Move);
        _task.AddTask(SquirelTask.Attack);
    }
}
