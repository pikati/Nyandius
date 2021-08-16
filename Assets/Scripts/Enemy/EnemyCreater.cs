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

    [SerializeField]
    private EnemyPopInfo[] _popInfo;
    private int _popIndex = 0;

    private RatController _rat;
    private RabitController _rabbit;
    private BirdController _bird;
    private DogController _dog;
    private TurtleController _turtle;
    private VolcanoController _volcano;
    private SharkController _shark;

    private GameTimer _popTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var popObj = GameObject.Find("EnemyPopInfomations");
        _rat = popObj.GetComponent<RatController>();
        _rabbit = popObj.GetComponent<RabitController>();
        _bird = popObj.GetComponent<BirdController>();
        _dog = popObj.GetComponent<DogController>();
        _turtle = popObj.GetComponent<TurtleController>();
        _volcano = popObj.GetComponent<VolcanoController>();
        _shark = popObj.GetComponent<SharkController>();
        _popTimer = new GameTimer(_popInfo[_popIndex]._popTime);
        this.UpdateAsObservable()
            .Subscribe(_ => PopEnemy());
    }

    private void PopEnemy()
    {
        if (!_popTimer.UpdateTimer()) return;

        switch (_popInfo[_popIndex]._enemyPopEnum)
        {
            case EnemyPopEnum.Rat:
                _rat.CreateRat(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Rabbit:
                _rabbit.CreateRabbit(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Bird:
                _bird.CreateBird(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Dog:
                _dog.CreateDog(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Turtle:
                _turtle.CreateTurtle(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Volcano:
                _volcano.CreateVolcano(_popInfo[_popIndex]._hegiht);
                break;
            case EnemyPopEnum.Shark:
                _shark.CreateShark(_popInfo[_popIndex]._hegiht);
                break;
            default:
                break;
        }
        _popIndex++;
        _popTimer.ResetTimer(_popInfo[_popIndex]._popTime);
    }
}
