using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Death();
        Destroy(gameObject,1.5f);
    }

    private void Death()
    {
        if (TryGetComponent(out BoxCollider2D collider))
            collider.enabled = false;

        _animator.Play("Death");
    }
}
