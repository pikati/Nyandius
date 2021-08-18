using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class GameFacilitator : Singleton<GameFacilitator>
{

    [SerializeField]
    private GameObject _title;
    [SerializeField]
    private GameObject _credit;
    [SerializeField]
    private GameObject _main;
    [SerializeField]
    private GameObject _result;
    [SerializeField]
    private GameObject _mainObjs;
    private bool _isTitleVisible = false;
    private GameStateController _gameStateController;

    private void Start()
    {
        _gameStateController = new GameStateController();
        _main.SetActive(false);
        _result.SetActive(false);
        _mainObjs.SetActive(false);
        VisibleChange();
    }

    public void VisibleChange()
    {
        _isTitleVisible = !_isTitleVisible;
        _title.SetActive(_isTitleVisible);
        _credit.SetActive(!_isTitleVisible);
    }

    public void StartMain()
    {
        _gameStateController.ChangeGameState(GameStateController.GameStateEnum.Game);
        _isTitleVisible = false;
        _title.SetActive(false);
        _credit.SetActive(false);
        _mainObjs.SetActive(true);
        _main.SetActive(true);
    }

    public GameStateController.GameStateEnum GetGameState()
    {
        return _gameStateController.GameState;
    }

    public void DispResult()
    {
        _gameStateController.ChangeGameState(GameStateController.GameStateEnum.Result);
        _main.SetActive(false);
        _mainObjs.SetActive(false);
        _result.SetActive(true);
    }

    public void ToTitle()
    {
        _gameStateController.ChangeGameState(GameStateController.GameStateEnum.Title);
        _result.SetActive(false);
        _title.SetActive(true);
        Singleton<LifeManager>.Instance?.Reset();
    }

    public void ToRanking()
    {
        var score = Singleton<ScoreManager>.Instance.Score.Value;
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
    }

}
