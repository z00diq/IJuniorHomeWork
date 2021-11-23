using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReloadTransition : Transition
{
    [SerializeField] private float _blockCooldown;

    private float _delay;

    private void Update()
    {
        if (_delay <= 0)
        {
            NeedTransit = true;
            _delay = _blockCooldown;
        }
        _delay -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _delay = _blockCooldown;
    }

    private void OnDisable()
    {
        NeedTransit = false;
    }
}
