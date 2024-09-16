using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IInteracrable
{
    [SerializeField] GameObject _player;
    [SerializeField] [Range(0f, 100f)] float _distanceField = 15f;
    [SerializeField] [Range(0f, 10f)] float _speed = 4f;
    Quaternion _rotation;
    Vector3 _direction;
    float _distance;
    Vector3 _enemyTarget;
    Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        RotateEnemy();

        _distance = Vector3.Distance(_player.transform.position, transform.position);
        if (_distance < _distanceField)
        {
            _enemyTarget = _player.transform.position;
        }
        else if (_startPos != transform.position)
        {
            _enemyTarget = _startPos;
        }

        MoveEnemy();
    }

    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemyTarget, _speed * Time.deltaTime);
    }
    void RotateEnemy()
    {
        _direction = _enemyTarget - transform.position;

        float targetAngle = Mathf.Atan2(_enemyTarget.x - transform.position.x, _enemyTarget.z - transform.position.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, targetAngle, 0);
    }

    public void InteractWithCollision()
    {
        Debug.Log("Hit!");
    }
    public void InteractWithTrigger()
    {
    }
}
