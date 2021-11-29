using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Grenade : Ammunition
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _explosionDistance;
    [SerializeField] private ContactFilter2D _contactFilter;

    private Rigidbody2D _rigidbody;
    private List<RaycastHit2D> _hits = new List<RaycastHit2D>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explosion();
        Destroy(gameObject);
    }

    private void Explosion()
    {
        var startExplosionPoint = transform.position + new Vector3(_explosionDistance, 0, 0);
        var endExplosionPoint = transform.position + new Vector3(-_explosionDistance, 0, 0);

        Physics2D.Linecast(startExplosionPoint, endExplosionPoint, _contactFilter, _hits);
       
        foreach (var enemyObject in _hits)
        {
            if(enemyObject.collider.gameObject.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
        }

        _hits.Clear();
    }
}
