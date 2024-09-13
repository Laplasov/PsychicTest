using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NewPlayer : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _InputSpace = 5f;
    [SerializeField] Camera _camera;

    Rigidbody _rb;
    float _inputHorizontal;
    float _inputVertical;
    bool _isInputSpace = false;
    bool _isCol;
    float _angle;
    Vector3 _mousePos;
    Vector3 _screenPosition;
    Quaternion _offset;
    Vector3 _playerPos;
    Vector3 _direction;
    Ray cursorRay;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    void Update()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _isInputSpace = true; 
        } 

        RotatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            _rb.AddForce(new Vector3(_inputHorizontal * _speed, 0, _inputVertical * _speed));

        }
        if (_isInputSpace && _isCol)
        {
            _rb.velocity += Vector3.up * _InputSpace;
            _isInputSpace = false;
        }
    }

    void RotatePlayer()
    {
        cursorRay = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 hitPoint;
        if (Physics.Raycast(cursorRay.origin, cursorRay.direction, out hit, float.PositiveInfinity))
        {
            hitPoint = hit.point;
            Vector3 direction = hitPoint - transform.position;

            //Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = lookRotation;

            float targetAngle = Mathf.Atan2(hitPoint.x - transform.position.x, hitPoint.z - transform.position.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, targetAngle, 0);

            Debug.Log(hitPoint);
            Debug.DrawRay(_camera.transform.position, hitPoint - _camera.transform.position, Color.green);
            Debug.DrawRay(transform.position, hitPoint - transform.position, Color.red);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        _isCol =  true;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        _isCol = false;
    }
}