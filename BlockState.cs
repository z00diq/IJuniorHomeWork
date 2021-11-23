using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockState : State
{
    [SerializeField] private Shield _shield;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Block();
    }

    private void Block()
    {
        _shield.gameObject.SetActive(true);
        _animator.Play("Block");
    }

    private void OnDisable()
    {
        _shield.gameObject.SetActive(false);
    }
}
