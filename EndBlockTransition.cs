using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlockTransition : Transition
{
    [SerializeField] private float _blockDuration;

    private float _delay=0;
    
    
    void Update()
    {
        if (_delay >= _blockDuration)
            NeedTransit = true;

        _delay += Time.deltaTime;
    }

    private void OnDisable()
    {
        _delay = 0;
    }
}
