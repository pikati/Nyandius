using System;
using UniRx;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private readonly IntReactiveProperty _score = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> Score => _score;
    // Start is called before the first frame update
    void Start()
    {
        _score.AddTo(this);
    }

    public void AddScore(int score)
    {
        _score.Value += score;
    }

    public void ResetScore()
    {
        _score.Value = 0;
    }
}
