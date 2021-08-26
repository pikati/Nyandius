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
    private GameObject _clear;
    [SerializeField]
    private GameObject _mainObjs;
    private bool _isTitleVisible = false;
    private GameStateController _gameStateController;
    private GameManager _gameManager;
    public bool CanInput { get; set; } = false;

    private void Start()
    {
        _gameManager = Singleton<GameManager>.Instance;
        _gameStateController = new GameStateController();
        _main.SetActive(false);
        _result.SetActive(false);
        _mainObjs.SetActive(false);
        _clear.SetActive(false);
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
        Singleton<CriSoundManager>.Instance.PlayBGM(CueID.Intoro);
        _isTitleVisible = false;
        _title.SetActive(false);
        _credit.SetActive(false);
        _mainObjs.SetActive(true);
        _main.SetActive(true);
        _gameManager.ResetLoopNum();
        _gameManager.StartGame();
    }

    public GameStateController.GameStateEnum GetGameState()
    {
        return _gameStateController.GameState;
    }

    public void DispResult(bool isClear = false)
    {
        
        _gameManager.GameClear();
        if (isClear)
        {
            _clear.SetActive(true);
        }
        _main.SetActive(false);
        _mainObjs.SetActive(false);
        _result.SetActive(true);
        _gameStateController.ChangeGameState(GameStateController.GameStateEnum.Result);
        Singleton<ScoreManager>.Instance.SetHiScore();

    }

    public void ToTitle()
    {
        if (CanInput) return;
        _gameStateController.ChangeGameState(GameStateController.GameStateEnum.Title);
        _result.SetActive(false);
        _clear.SetActive(false);
        _title.SetActive(true);
        Singleton<LifeManager>.Instance.Reset();
        Singleton<CriSoundManager>.Instance.StopBGM();
    }

    public void ToRanking()
    {
        if (CanInput) return;
        var score = Singleton<ScoreManager>.Instance.Score.Value;
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        CanInput = true;
    }

}
