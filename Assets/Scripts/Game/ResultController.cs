using UniRx;
using UniRx.Triggers;
using UnityEngine;

public enum ResultButton
{
    ToTitle,
    Ranking,
    Max
}

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _cursor;
    private IntReactiveProperty _index = new IntReactiveProperty(0);
    private InputController _ic;
    private bool _isInput = true;
    private GameTimer _timer = new GameTimer(1.0f);

    void Start()
    {
        _ic = Singleton<InputController>.Instance;
        this.UpdateAsObservable()
            .Subscribe(_ => CheckInput())
            .AddTo(this);
        _index
            .Subscribe(_ => ChangeCursorPosition())
            .AddTo(this);
    }

    private void CheckInput()
    {
        if (Singleton<GameFacilitator>.Instance.GetGameState() != GameStateController.GameStateEnum.Result) return;
        if (Singleton<GameFacilitator>.Instance.CanInput) return;
        if (!_timer.UpdateTimer()) return;
        if (_ic.A)
        {
            if (_isInput)
            {
                return;
            }
            _isInput = true;
            OnSelect();
        }
        if (_ic.MoveValue.x > 0.7f || _ic.ArrowValue.x > 0)
        {
            if (_isInput)
            {
                return;
            }
            _isInput = true;
            MoveCursor(1);
        }
        else if (_ic.MoveValue.x < -0.7f || _ic.ArrowValue.x < 0)
        {
            if (_isInput)
            {
                return;
            }
            _isInput = true;
            MoveCursor(-1);
        }
        else
        {
            _isInput = false;
        }
    }

    private void ChangeCursorPosition()
    {
        var x = -800;
        if (_index.Value == 1)
        {
            x = 100;
        }
        _cursor.anchoredPosition = new Vector3(x, _cursor.anchoredPosition.y, 0);
    }

    private void OnSelect()
    {
        Singleton<CriSoundManager>.Instance.PlaySE(CueID.Decide);
        switch ((ResultButton)_index.Value)
        {
            case ResultButton.ToTitle:
                Singleton<GameFacilitator>.Instance.ToTitle();
                Singleton<CriSoundManager>.Instance.StopBGM();
                break;
            case ResultButton.Ranking:
                Singleton<GameFacilitator>.Instance.ToRanking();
                break;
            default:
                break;
        }
    }

    private void MoveCursor(int moveValue)
    {
        Singleton<CriSoundManager>.Instance.PlaySE(CueID.Decide);
        _index.Value += moveValue;
        var max = (int)TitleButtonState.Max;
        if (_index.Value > max)
        {
            _index.Value = max - 1;
        }
        else if (_index.Value < 0)
        {
            _index.Value = 0;
        }
    }
}
