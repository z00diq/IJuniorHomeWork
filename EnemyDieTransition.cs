using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDieTransition : Transition
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
