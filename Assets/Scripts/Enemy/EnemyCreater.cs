using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    public enum EnemyPopEnum 
    {
        Rat,
        Rabbit,
        Bird,
        Dog,
        Turtle,
        Volcano,
        Shark
    }

    [System.Serializable]
    public class EnemyPopInfo
    {
        public EnemyPopEnum _enemyPopEnum;
        public float _hegiht;
        public float _popTime;
    }

    [System.Serializable]
    public class EnemyPopInfos
    {
        public EnemyPopInfo[] _enemyPopInfo;
    }


    [SerializeField]
    private EnemyPopInfos[] _popInfos;
    [SerializeField]
    private int[] _middleBossIndex;
    [SerializeField]
    private int[] _bossIndex;
    private int _popIndex = 0;

    private RatController _rat;
    private RabitController _rabbit;
    private BirdController _bird;
    private DogController _dog;
    private TurtleController _turtle;
    private VolcanoController _volcano;
    private SharkController _shark;

    private GameTimer _popTimer;

    private GameManager _gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = Singleton<GameManager>.Instance;
        var popObj = GameObject.Find("EnemyPopInfomations");
        _rat = popObj.GetComponent<RatController>();
        _rabbit = popObj.GetComponent<RabitController>();
        _bird = popObj.GetComponent<BirdController>();
        _dog = popObj.GetComponent<DogController>();
        _turtle = popObj.GetComponent<TurtleController>();
        _volcano = popObj.GetComponent<VolcanoController>();
        _shark = popObj.GetComponent<SharkController>();
        _popTimer = new GameTimer(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._popTime);
        this.UpdateAsObservable()
            .Subscribe(_ => PopEnemy());
    }

    private void PopEnemy()
    {
        if (_gameManager.IsPlayerDead() || Singleton<GameFacilitator>.Instance.GetGameState() != GameStateController.GameStateEnum.Game) return;
        if (!_popTimer.UpdateTimer()) return;

        switch (_popInfos[_popIndex]._enemyPopInfo[_popIndex]._enemyPopEnum)
        {
            case EnemyPopEnum.Rat:
                _rat.CreateRat(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Rabbit:
                _rabbit.CreateRabbit(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Bird:
                _bird.CreateBird(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Dog:
                _dog.CreateDog(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Turtle:
                _turtle.CreateTurtle(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Volcano:
                _volcano.CreateVolcano(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Shark:
                _shark.CreateShark(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._hegiht);
                break;
            default:
                break;
        }
        _popIndex++;
        _popTimer.ResetTimer(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._popTime);
    }

    public void Reset()
    {
        _popIndex = 0;
        _popTimer = new GameTimer(_popInfos[_popIndex]._enemyPopInfo[_popIndex]._popTime);
    }
}
