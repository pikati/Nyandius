using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    public enum EnemyPopEnum 
    {
        Rat
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

    private GameTimer _popTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rat = GameObject.Find("EnemyPopInfomations").GetComponent<RatController>();
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

        }
        _popIndex++;
        _popTimer.ResetTimer(_popInfo[_popIndex]._popTime);
    }
}
