using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Vector3 _startPosition;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = Singleton<GameManager>.Instance;
        _startPosition = transform.position;
        this.UpdateAsObservable()
            .Subscribe(_ => UpdatePosition())
            .AddTo(this);
    }

    private void UpdatePosition()
    {
        if (_gameManager.IsPlayerDead())
        {
            transform.position = _startPosition;
        }
        if (_gameManager.BossTimer.IsTimeUp && !_gameManager.IsBoss)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }
}
