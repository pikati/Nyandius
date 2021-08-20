using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Rabbit : Enemy
{
    private enum RabbitTask
    {
        Move
    }
    TaskList<RabbitTask> _task = new TaskList<RabbitTask>();
    private float _speed = 4.0f;
    private float _rad = 1.0f;
    private float _angle = 0;
    private float _posY;
    private GameManager _gm;
    protected override void Initialize()
    {
        _gm = Singleton<GameManager>.Instance;
        _score = 100 + (_gm.LoopNum * 100);
        _hp.Value = 1 + (_gm.LoopNum * 6);
        _posY = transform.position.y;
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
        base.Initialize();
    }

    private void OnTaskMoveEnter()
    {

    }

    private bool OnTaskMoveUpdate()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x - _speed * Time.deltaTime, _posY + _rad * Mathf.Sin(_angle), 0);
        _angle += 0.0075f + (_gm.LoopNum * 0.002f);
        if(_angle > Mathf.PI * 2)
        {
            _angle -= Mathf.PI * 2;
        }
        return IsOffScreen() && pos.x < -10.0f;
    }

    private void OnTaskMoveExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(RabbitTask.Move, OnTaskMoveEnter, OnTaskMoveUpdate, OnTaskMoveExit);
    }

    private void SetTask()
    {
        _task.AddTask(RabbitTask.Move);
    }
}
