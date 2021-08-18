using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public enum TitleButtonState
{
    Start,
    Credit,
    End,
    Max
}


public class TitleController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _cursor;
    private IntReactiveProperty _index = new IntReactiveProperty(0);
    private InputController _ic;
    private bool _isInput = false;
    // Start is called before the first frame update
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
        if (Singleton<GameFacilitator>.Instance.GetGameState() == GameStateController.GameStateEnum.Game) return;
        if(_ic.A)
        {
            OnSelect();
        }
        if (_ic.MoveValue.y > 0.7f || _ic.ArrowValue.y > 0)
        {
            if(_isInput)
            {
                return;
            }
            _isInput = true;
            MoveCursor(-1);
        }
        else if (_ic.MoveValue.y < -0.7f || _ic.ArrowValue.y < 0)
        {
            if (_isInput)
            {
                return;
            }
            _isInput = true;
            MoveCursor(1);
        }
        else
        {
            _isInput = false;
        }
    }

    private void ChangeCursorPosition()
    {
        var y = -60;
        if(_index.Value == 1)
        {
            y = -220;
        }
        else if(_index.Value == 2)
        {
            y = -380;
        }
        _cursor.anchoredPosition = new Vector3(_cursor.anchoredPosition.x, y, 0);
    }

    private void OnSelect()
    {
        switch ((TitleButtonState)_index.Value)
        {
            case TitleButtonState.Start:
                Singleton<GameFacilitator>.Instance.StartMain();
                break;
            case TitleButtonState.Credit:
                Singleton<GameFacilitator>.Instance.VisibleChange();
                break;
            case TitleButtonState.End:
                Quit();
                break;
            default:
                break;
        }
    }

    private void MoveCursor(int moveValue)
    {
        _index.Value += moveValue;
        var max = (int)TitleButtonState.Max;
        if (_index.Value > max)
        {
            _index.Value = max - 1;
        }
        else if(_index.Value < 0)
        {
            _index.Value = 0;
        }
    }
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
