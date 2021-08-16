using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Turtle : Enemy
{
    private enum TurtleTask
    {
        Attack
    }
    TaskList<TurtleTask> _task = new TaskList<TurtleTask>();
    private readonly float _createInterval = 1.5f;
    private GameTimer _createTimer;
    private GameObject _squirrel;
    private float _maxHP;

    protected override void Initialize()
    {
        _score = 800;
        _hp.Value = 6;
        _maxHP = _hp.Value;
        _createTimer = new GameTimer(_createInterval);
        _squirrel = Resources.Load("Enemy/Squirrel") as GameObject;
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
        if (pos.x < 10.0f)
        {
            if (_createTimer.UpdateTimer())
            {
                Instantiate(_squirrel, transform.position, Quaternion.identity);
                _createTimer.ResetTimer();
            }
        }
        transform.position += Vector3.left * 1.5f * Time.deltaTime;
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
        _task.DefineTask(TurtleTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);
    }

    private void SetTask()
    {
        _task.AddTask(TurtleTask.Attack);
    }
}
