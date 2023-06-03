using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack();
}

public interface IReload
{
    void Reload();
}

public interface IEnemyState
{
    void OnEntry(EnemyBehavior enemy);
    void OnUpdate(EnemyBehavior enemy);
    void OnExit(EnemyBehavior enemy);
}