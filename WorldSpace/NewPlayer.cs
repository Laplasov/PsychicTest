using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NewPlayer : MonoBehaviour
{
    [SerializeField][Range(0f, 20f)] float _speed;
    [SerializeField][Range(0f, 20f)] float _speedSpace;
    [SerializeField] Camera _camera;
    [SerializeField][Range(0f, 20f)] float _maxAdditionalForce;

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
    Ray _cursorRay;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = 10f;
        _speedSpace = 5f;
        _maxAdditionalForce = 12f;
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
        MoveCamera();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    void MoveCamera()
    {
        _camera.transform.position = new Vector3(
            transform.position.x, 
            _camera.transform.position.y, 
            transform.position.z);
    }
    void MovePlayer()
    {
        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            Vector3 additionalForce = new Vector3(_inputHorizontal * _speed, 0, _inputVertical * _speed);
            if (additionalForce.magnitude > _maxAdditionalForce)
            {
                additionalForce = additionalForce.normalized * _maxAdditionalForce;
            }
            _rb.AddForce(additionalForce);
        }
        if (_isInputSpace && _isCol)
        {
            _rb.velocity += Vector3.up * _speedSpace;
            _isInputSpace = false;
        }
    }

    void RotatePlayer()
    {
        _cursorRay = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 hitPoint;

        if (Physics.Raycast(_cursorRay.origin, _cursorRay.direction, out hit, float.PositiveInfinity))
        {
            hitPoint = hit.point;
            Vector3 direction = hitPoint - transform.position;
            //Debug.Log(hitPoint);

            //Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = lookRotation;

            float targetAngle = Mathf.Atan2(hitPoint.x - transform.position.x, hitPoint.z - transform.position.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, targetAngle, 0);

            //Debug.DrawRay(_camera.transform.position, hitPoint - _camera.transform.position, Color.green);
            //Debug.DrawRay(transform.position, hitPoint - transform.position, Color.red);
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.TryGetComponent(out IInteracrable interactableColider)) {
            interactableColider.InteractWithCollision();
            if (collisionInfo.transform.tag == "Enemy")
            {
                Destroy(collisionInfo.gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Ground")
        {
            _isCol =  true;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Ground")
        {
            _isCol = false;
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.TryGetComponent(out IInteracrable interactableColider))
        {
            interactableColider.InteractWithTrigger();
        }
    }
}