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
    void OnEntry(Enemy enemy);
    void OnUpdate(Enemy enemy);
    void OnExit(Enemy enemy);
}