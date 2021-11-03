using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;
   
    [SerializeField] private float _speed;

    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rb2d;
    private Animator _animator;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;


    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(LayerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _animator.SetBool("Run", false);
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0)*_speed;
        
        if (_targetVelocity.x != 0 && _grounded)
        {
            _animator.SetBool("Run", true);

            if (_targetVelocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (_targetVelocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
        if (Input.GetKey(KeyCode.Space) && _grounded)
            Velocity.y = 6.5f;
        
    }

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
        
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (_hitBufferList[i].normal.y > MinGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }

                    float projection = Vector2.Dot(Velocity, currentNormal);

                    if (projection < 0)
                    {
                        Velocity = Velocity - projection * currentNormal;
                    }

                    float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }

                
            }
        }
        
        _rb2d.position = _rb2d.position + move.normalized * distance;
        


    }
}