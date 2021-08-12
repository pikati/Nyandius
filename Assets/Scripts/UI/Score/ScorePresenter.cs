using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private ScoreView _view;

    private void Start()
    {
        _scoreManager.Score
            .Subscribe(x => _view.ChangeScoreText(x))
            .AddTo(this);
    }
}
