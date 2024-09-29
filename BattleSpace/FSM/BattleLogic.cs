using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogic : MonoBehaviour
{
    BaseBattleState _currentState;
    public BeginState BeginState = new BeginState();
    public PlayerState PlayerState = new PlayerState();
    public EnemyState EnemyState = new EnemyState();
    public EndBattleState EndBattleState = new EndBattleState();

    [SerializeField]
    private TMP_Text _massage;
    [SerializeField]
    private GameObject _optionsContent;
    [SerializeField]
    private PlayerUnit _playerUnit;
    [SerializeField]
    public GameObject[] EnemySpown;
    [HideInInspector]
    public List<EnemyUnit> EnemyUnits = new List<EnemyUnit>();
    public TMP_Text Massage
    {
        get { return _massage; }
        private set { _massage = value; }
    }
    public GameObject OptionsContent
    {
        get { return _optionsContent; }
        private set { _optionsContent = value; }
    }
    public PlayerUnit PlayerUnit
    {
        get { return _playerUnit; }
        private set { _playerUnit = value; }
    }

    void Start()
    {
        _currentState = BeginState;
        if (_currentState != null)
            _currentState.EnteringState(this);
    }


    void Update()
    {
        if (_currentState != null)
            _currentState.UpdateState(this);
        
    }
    public void SwitchState(BaseBattleState state)
    {
        _currentState = state;
        state.EnteringState(this);
    }

}
