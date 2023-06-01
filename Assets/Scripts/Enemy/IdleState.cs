using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    public void OnEntry(Enemy enemy)
    {
        // This will be called when first entering the state
    }
    public void OnUpdate(Enemy enemy)
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    enemy.ChangeState(enemy.patrolState);
        //}
        enemy.ChangeState(enemy.patrolState);
    }
    public void OnExit(Enemy enemy)
    {
        // This will be called on leaving the state
    }
}
