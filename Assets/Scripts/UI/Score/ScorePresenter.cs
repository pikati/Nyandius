using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private ScoreView _mainView;
    [SerializeField]
    private ScoreView _resultView;

    private void Start()
    {
        var f = Singleton<GameFacilitator>.Instance;
        _scoreManager.Score
            .Subscribe(x => ChangeScore(x))
            .AddTo(this);
        _scoreManager.HiScore
            .Subscribe(x => ChangeHiScore(x))
            .AddTo(this);
    }

    private void ChangeScore(int score)
    {
        _mainView.ChangeScore(score);
        _resultView.ChangeScore(score);
    }

    private void ChangeHiScore(int hiSocre)
    {
        _mainView.ChangeHiScore(hiSocre);
        _resultView.ChangeHiScore(hiSocre);
    }
}
