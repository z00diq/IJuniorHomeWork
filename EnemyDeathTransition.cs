using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTransition : Transition
{

    private Enemy _enemy;


    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        NeedTransit = true;
    }
}
