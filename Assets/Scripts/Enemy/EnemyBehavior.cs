using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : Enemy
{
    public IEnemyState currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();

    void Start()
    {
        patrolState.targetName = targetName;
        chaseState.targetName = targetName;
        patrolState.loseDistance = minimunTraceDistance;
        chaseState.loseDistance = minimunTraceDistance;
        ChangeState(idleState);
    }

    void Update()
    {
        currentState.OnUpdate(this);        
    }  

    // 方法：改變狀態
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEntry(this);
    }
}
