using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    private Animator _animator;
    private const string TargetState = "Death";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Dying();
        Destroy(gameObject,1.5f);
    }

    private void Dying()
    {
        if (TryGetComponent(out BoxCollider2D collider))
            collider.enabled = false;

        _animator.Play(targetState);
    }
}
