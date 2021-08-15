using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Turtle : Enemy
{
    private enum DogTask
    {
        Attack
    }
    TaskList<DogTask> _task = new TaskList<DogTask>();
    private readonly float _createInterval = 1.5f;
    private GameTimer _createTimer;
    private GameObject _squirrel;

    protected override void Initialize()
    {
        _score = 800;
        _hp.Value = 6;
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
