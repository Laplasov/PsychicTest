using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static NewPlayer;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyScript : MonoBehaviour, IInteracrable
{
    GameObject _player;
    Quaternion _rotation;
    Vector3 _direction;
    float _distance;
    Vector3 _enemyTarget;
    Vector3 _startPos;
    bool _waitOnStart = true;
    bool _wasHit = false;

    public float DistanceField;
    public float Speed;
    public EnemyUnitStats[] EnemyUnits;

    void Awake()
    {
        //_wasHit = false;
        //if (InitBattleData.BattleScene)
        //    Destroy(this);
    }
    void Start() => StartCoroutine(Waiting());
    private void OnEnable() => StartCoroutine(Waiting());

    void Update()
    {
        if (_waitOnStart == true) return;
        _startPos = transform.parent.position;
        TargetHandler();
        if (Vector3.Distance(transform.position, _startPos) < 1f && _enemyTarget == _startPos) return;
        RotateEnemy();
        MoveEnemy();
    }
    IEnumerator Waiting() {
        _waitOnStart = true;

        if (_wasHit == true) EnemyUnits = InitBattleData.EnemyUnitsInitData.ToArray();
        yield return null;
        if (EnemyUnits.Length == 0)
            Destroy(this.gameObject);
        yield return new WaitForSeconds(2.5f);
        _player = InitBattleData.Player;
        _wasHit = false;
        _waitOnStart = false;
    }

    void MoveEnemy()
    {
        //float newX = Mathf.MoveTowards(transform.position.x, _enemyTarget.x, Speed * Time.deltaTime);
        //float newZ = Mathf.MoveTowards(transform.position.z, _enemyTarget.z, Speed * Time.deltaTime);

        //transform.position = new Vector3(newX, transform.position.y, newZ);
        Vector3 ZXPos = _enemyTarget;
        ZXPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, ZXPos, Speed * Time.deltaTime);
    }
    void TargetHandler() 
    {
        _distance = Vector3.Distance(_player.transform.position, transform.position);
        if (_distance < DistanceField)
        {
            _enemyTarget = _player.transform.position;
        }
        else if (_startPos != transform.position)
        {
            _enemyTarget = _startPos;
        }
    }
    void RotateEnemy()
    {
        _direction = _enemyTarget - transform.position;

        float targetAngle = Mathf.Atan2(_enemyTarget.x - transform.position.x, _enemyTarget.z - transform.position.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, targetAngle, 0);
    }
    public void InteractWithCollision()
    {
        if (_waitOnStart != true) 
        {
            _wasHit = true;
            InitBattleData.EnemyUnitsInitData = EnemyUnits.ToList();
            _player.GetComponent<Inventory>().LoadActionsToBattleInit();
            SceneManagerSinglton.Instans.LoadNextLevel();
        }
    }
    public void InteractWithTrigger() { }
    public void InteractWithTriggerStay() { }
    public void InteractWithTriggerExit() { }
}
