using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;
using static NewPlayer;

public class EnemyScript : MonoBehaviour, IInteracrable
{
    GameObject _player;
    [SerializeField][Range(0f, 100f)] float _distanceField = 15f;
    [SerializeField][Range(0f, 10f)] float _speed = 4f;
    Quaternion _rotation;
    Vector3 _direction;
    float _distance;
    Vector3 _enemyTarget;
    Vector3 _startPos;
    bool _waitOnStart = true;
    bool _wasHit = true;

    [SerializeField]
    private EnemyUnitScriptableObject _enemyUnitSO;
    [SerializeField]
    public EnemyUnitScriptableObject[] EnemyUnits;


    private void Awake()
    {
        _wasHit = false;
        GeneratRandomInstansesOfUnits();
        StartCoroutine(Waiting());
    }
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

        if (EnemyUnits.Length == 0)
            Destroy(this.gameObject);
        yield return new WaitForSeconds(2.5f);
        _player = InitBattleData.Player;
        _wasHit = false;
        _waitOnStart = false;
    }

    void MoveEnemy()
    {
        //float newX = Mathf.MoveTowards(transform.position.x, _enemyTarget.x, _speed * Time.deltaTime);
        //float newZ = Mathf.MoveTowards(transform.position.z, _enemyTarget.z, _speed * Time.deltaTime);

        //transform.position = new Vector3(newX, transform.position.y, newZ);
        Vector3 ZXPos = _enemyTarget;
        ZXPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, ZXPos, _speed * Time.deltaTime);
    }
    void TargetHandler() 
    {
        _distance = Vector3.Distance(_player.transform.position, transform.position);
        if (_distance < _distanceField)
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
    private void GeneratRandomInstansesOfUnits()
    {
        int enemyCount = Random.Range(1, 4);
        EnemyUnits = new EnemyUnitScriptableObject[enemyCount];
        int i = 0;
        foreach (EnemyUnitScriptableObject Unity in EnemyUnits)
        {
            EnemyUnits[i] = _enemyUnitSO;
            i++;
        }
    }

    public void InteractWithCollision()
    {
        if (_waitOnStart != true) 
        {
            Debug.Log("Hit!");
            _wasHit = true;
            InitBattleData.EnemyUnitsInitData = EnemyUnits.ToList();
            SceneManagerSinglton.Instans.LoadNextLevel();
        }
    }
    public void InteractWithTrigger()
    {
    }
}
