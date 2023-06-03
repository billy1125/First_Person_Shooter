using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI

[System.Serializable]
public class ChaseState : IEnemyState
{
    [Header("追擊狀態設定")]
    [SerializeField] float minimunAttackDistance = 5;
    [SerializeField] Transform patrolPoints;

    Transform myTransform;
    string targetName;
    float minimunChaseDistance;
    GameObject player;
    NavMeshAgent navMeshAgent;

    public void OnEntry(Enemy enemy)
    {
        Debug.Log("ChaseState");
        myTransform = enemy.transform;
        targetName = enemy.targetName;
        minimunChaseDistance = enemy.minimunTraceDistance;
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        player = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
    }

    public void OnUpdate(Enemy enemy)
    {      
        Chase();
        if (PlayerInAttackArea())
            enemy.ChangeState(enemy.attackState);
        else if (PlayerLost())
        {
            BackToPatrolArea();
            float distance = Vector3.Distance(myTransform.position, patrolPoints.position);
            if (distance <= 1.0f)
            {
                enemy.ChangeState(enemy.patrolState);
            }
        }   
    }

    public void OnExit(Enemy enemy)
    {
        navMeshAgent.enabled = false;
    }

    public void SetTarget(NavMeshAgent _navMeshAgent)
    {
        navMeshAgent = _navMeshAgent;
    }

    private void Chase()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(player.transform.position);    // 讓自己往目標物的座標移動   
        //myTransform.position = Vector3.MoveTowards(myTransform.position, target.position, chaseSpeed * Time.deltaTime);
    }

    private void BackToPatrolArea()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(patrolPoints.position);    // 讓自己往目標物的座標移動  
    }

    private bool PlayerLost()
    {
        //if (!target)
        //{
        //    return true;
        //}
        //if (Vector3.Distance(myTransform.position, target.position) > loseDistance)
        //{
        //    return true;
        //}
        //return false;

        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(player.transform.position, myTransform.position);

        // 判斷式：判斷距離是否低於最短追蹤距離，如果與目標的距離大於最小距離，就不追蹤，否則就開始追蹤
        if (distance > minimunChaseDistance)
            return true;
        else
            return false;
    }

    private bool PlayerInAttackArea()
    {
        float distance = Vector3.Distance(player.transform.position, myTransform.position);

        if (distance <= minimunAttackDistance)
            return true;
        else
            return false;
    }
}
