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
    private Vector3 _direction;
    private float _speed;
    private float _maxHP;
    private GameManager _gameManager;
    private EnemyCreater _enemyCreater;
    private BGController _BGController;
    private PlayerMove _playerMove;
    private bool _isInvicible = true;

    protected override void Initialize()
    {
        _score = 2000 + (_gameManager.LoopNum * 2000);
        _hp.Value = 50 + (_gameManager.LoopNum * 75);
        _maxHP = _hp.Value;
        _shotTimer = new GameTimer();
        _speed = 2.0f;
        _gameManager = Singleton<GameManager>.Instance;
        _enemyCreater = GameObject.Find("EnemyPopManager").GetComponent<EnemyCreater>();
        _BGController = GameObject.Find("BackGround").GetComponent<BGController>();
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        DefineTask();
        SetTask();
        this.UpdateAsObservable()
            .Subscribe(_ => _task.UpdateTask());
        base.Initialize();
    }

    private void OnTaskEnterEnter()
    {
        _isInvicible = true;
        _direction = Vector3.left;
    }

    private bool OnTaskEnterUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        if (transform.position.x < 5.0f)
        {
            return true;
        }
        return false;
    }

    private void OnTaskEnterExit()
    {
        _isInvicible = false;
    }

    private void OnTaskAttackEnter()
    {
        _direction = Vector3.up;
        _shotTimer.ResetTimer(Random.Range(0.5f, 1.0f));
    }

    private bool OnTaskAttackUpdate()
    {
        var pos = transform.position;
        if (pos.y > 3)
        {
            _direction = Vector3.down;
        }
        else if (pos.y < -3)
        {
            _direction = Vector3.up;
        }
        if (_shotTimer.UpdateTimer())
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.SetDirection(Vector3.left);
            bullet.SetSpeed(6.5f);
            if (_gameManager.LoopNum >= 1)
            {
                var bullet2 = Instantiate(_bullet, transform.position + new Vector3(0, 2, 0), Quaternion.identity).GetComponent<EnemyBullet>();
                bullet2.SetDirection(Vector3.left);
                bullet2.SetSpeed(5.5f);
                var bullet3 = Instantiate(_bullet, transform.position + new Vector3(0, -2, 0), Quaternion.identity).GetComponent<EnemyBullet>();
                bullet3.SetDirection(Vector3.left);
                bullet3.SetSpeed(5.5f);
            }
            if (_gameManager.LoopNum >= 3)
            {
                var bullet2 = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                bullet2.SetDirection(new Vector3(1, 1, 0).normalized);
                bullet2.SetSpeed(7.5f);
                var bullet3 = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                bullet3.SetDirection(new Vector3(1, -1, 0).normalized);
                bullet3.SetSpeed(7.5f);
            }
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

    public override void OnCollision(Collider col)
    {
        if (_isInvicible) return;
        if (col.CompareTag("Bullet"))
        {
            OnDamage(col.GetComponent<Bullet>().Damage());
        }
    }

    protected override void OnDead()
    {
        _enemyCreater.Reset();
        _BGController.ResetPosition();
        _playerMove.ResetPosition();
        base.OnDead();
    }

    private void OnBecameInvisible()
    {
        _gameManager.CountLoop();
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
    }
}
