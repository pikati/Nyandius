using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Bird : Enemy
{
    private enum BirdTask
    {
        Move,
        Attack
    };

    TaskList<BirdTask> _task = new TaskList<BirdTask>();
    private Transform _playerTransform;
    private Vector3 _moveDirection;
    private float _speed = 3.0f;
    private GameManager _gm;

    protected override void Initialize()
    {
        _gm = Singleton<GameManager>.Instance;
        _score = 200 * (_gm.LoopNum * 50);
        _hp.Value = 1 + _gm.LoopNum;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        var pPos = _playerTransform.position;
        var pos = transform.position;
        if (pPos.y < pos.y - 0.25f)
        {
            _moveDirection = new Vector3(-1.0f, -0.2f, 0);
        }
        else if (pPos.y > pos.y + 0.25f)
        {
            _moveDirection = new Vector3(-1.0f, 0.2f, 0);
        }
        else
        {
            if (transform.position.x - _playerTransform.position.x < 4)
            {
                return true;
            }
        }
        transform.position += _moveDirection * Time.deltaTime * _speed;
        return false;
    }

    private void OnTaskMoveExit()
    {

    }

    private void OnTaskAttackEnter()
    {
        _moveDirection = new Vector3(1 + 0.25f * _gm.LoopNum, 0, 0);
    }

    private bool OnTaskAttackUpdate()
    {
        transform.position += _moveDirection * Time.deltaTime * _speed * 2;
        return IsOffScreen();
    }

    private void OnTaskAttackExit()
    {
        DestroyThis();
    }

    private void DefineTask()
    {
        _task.DefineTask(BirdTask.Move, OnTaskMoveEnter, OnTaskMoveUpdate, OnTaskMoveExit);
        _task.DefineTask(BirdTask.Attack, OnTaskAttackEnter, OnTaskAttackUpdate, OnTaskAttackExit);

    }

    private void SetTask()
    {
        _task.AddTask(BirdTask.Move);
        _task.AddTask(BirdTask.Attack);
    }
}
