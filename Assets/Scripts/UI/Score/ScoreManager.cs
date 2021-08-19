using System;
using UniRx;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private readonly IntReactiveProperty _score = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> Score => _score;

    private readonly IntReactiveProperty _hiScore = new IntReactiveProperty(0);
    public IReactiveProperty<int> HiScore => _hiScore;
    // Start is called before the first frame update
    void Start()
    {
        _score.AddTo(this);
        _hiScore.AddTo(this);
        _hiScore.Value = PlayerPrefs.GetInt("SCORE", 0);
    }

    public void AddScore(int score)
    {
        _score.Value += score;
    }

    public void ResetScore()
    {
        _score.Value = 0;
    }

    public void SetHiScore()
    {
        _hiScore.Value = _score.Value > _hiScore.Value ? _score.Value : _hiScore.Value ;
        PlayerPrefs.SetInt("SCORE", _hiScore.Value);
        PlayerPrefs.Save();
    }
}
