using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI

[System.Serializable]
public class ChaseState : IEnemyState
{
    [Header("追擊狀態設定")]
    [SerializeField] Transform patrolPoints;

    [HideInInspector] public Transform target;
    [HideInInspector] Transform myTransform;
    [HideInInspector] public string targetName;
    [HideInInspector] public float loseDistance;

    GameObject player;
    NavMeshAgent navMeshAgent;

    public void OnEntry(EnemyBehavior enemy)
    {
        myTransform = enemy.transform;
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        player = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
    }

    public void OnUpdate(EnemyBehavior enemy)
    {
        if (PlayerLost())
        {
            BackToPatrolArea();
            float distance = Vector3.Distance(myTransform.position, patrolPoints.position);
            if (distance <= 1.0f)
            {
                enemy.ChangeState(enemy.patrolState);
            }           
        }
        else
        {
            Chase();
        }
    }

    public void OnExit(EnemyBehavior enemy)
    {
        // "Must've been the wind"
    }

    public void SetTarget(NavMeshAgent _navMeshAgent)
    {
        navMeshAgent = _navMeshAgent;
    }

    void Chase()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(player.transform.position);    // 讓自己往目標物的座標移動   
        //myTransform.position = Vector3.MoveTowards(myTransform.position, target.position, chaseSpeed * Time.deltaTime);
    }

    void BackToPatrolArea()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(patrolPoints.position);    // 讓自己往目標物的座標移動  
    }

    bool PlayerLost()
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
        if (distance > loseDistance)
            return true;
        else
            return false;
    }
}
