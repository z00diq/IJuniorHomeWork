using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private  Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private float _minGroundNormalY = .65f;
    private float _gravityModifier = 1f;
    private const string _isRuning = "Run";
    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _animator.SetBool(_isRuning, false);
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0)*_speed;
        
        if (_targetVelocity.x != 0 && _grounded)
        {
            _animator.SetBool("Run", true);

            if (_targetVelocity.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_targetVelocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        
        if (Input.GetKey(KeyCode.Space) && _grounded)
            _velocity.y = 6.5f;    
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            Casting(move, distance);
            distance = FindMovingDistance(yMovement, distance);
        }
        _rigidBody.position = _rigidBody.position + move.normalized * distance;
    }

    private float FindMovingDistance(bool yMovement, float distance)
    {
        for (int i = 0; i < _hitBufferList.Count; i++)
        {
            Vector2 currentNormal = _hitBufferList[i].normal;

            if (_hitBufferList[i].normal.y > _minGroundNormalY)
            {
                _grounded = true;

                if (yMovement)
                {
                    _groundNormal = currentNormal;
                    currentNormal.x = 0;
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        return distance;
    }

    private void Casting(Vector2 move, float distance)
    {
        int count = _rigidBody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

        _hitBufferList.Clear();

        for (int i = 0; i < count; i++)
        {
            _hitBufferList.Add(_hitBuffer[i]);
        }
    }
}