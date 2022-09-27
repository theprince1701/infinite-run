using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float fallMultiplier;
    
    private Rigidbody _rigidbody;
    private bool _grounded;
    private bool _crouch;
    private Vector3 _defaultScale;
    private Vector3 _crouchScale;
    
    private int _jumpCount;
    
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _defaultScale = transform.localScale;

        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        _crouchScale = new Vector3(_defaultScale.x, _defaultScale.y * 0.45f, _defaultScale.z);
    }

    private void Update()
    {
        if (Game.Instance.State != Game.GameStates.GameStarted)
            return;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!_grounded && _jumpCount <= 2)
                _jumpCount++;

            if (_jumpCount <= 2)
            {
                float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * gravity);
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpSpeed,
                    _rigidbody.velocity.z);
            }
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _crouchScale,
                Time.deltaTime * 7f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _defaultScale, Time.deltaTime * 7);
        }
        
        transform.Translate(transform.forward * (Time.deltaTime * (speed + Game.Instance.Score / 600)), Space.World);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));

        _grounded = false;
    }
    
    void OnCollisionStay()
    {
        _grounded = true;
        _jumpCount = 0;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Game.Instance.UpdateGameState(Game.GameStates.GameOver);
        }
    }
}
