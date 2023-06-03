using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolState : IEnemyState
{
    [Header("巡邏狀態設定")]
    [SerializeField] float patrolSpeed = 5;
    [SerializeField] int waypoint;
    [SerializeField] List<Transform> waypoints;

    string targetName;
    float minimunChaseDistance;
    Transform myTransform;
    GameObject player;
    
    public void OnEntry(Enemy enemy)
    {
        //Debug.Log("PatrolState");
        waypoint = 0;
        myTransform = enemy.transform;
        targetName = enemy.targetName;
        minimunChaseDistance = enemy.minimunTraceDistance;
        enemy.animator.SetBool("Walk", true);
        player = GameObject.FindGameObjectWithTag(targetName);   
    }

    public void OnUpdate(Enemy enemy)
    {
        Patrol();
        if (PlayerInChaseArea())
        {
            enemy.ChangeState(enemy.chaseState);
        }
    }

    public void OnExit(Enemy enemy)
    {
        // This will be called when first entering the state
    }

    bool PlayerInChaseArea()
    {
        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(player.transform.position, myTransform.position);
        //Debug.Log(distance);
        // 判斷式：判斷距離是否低於最短追蹤距離，如果與目標的距離大於最小距離，就不追蹤，否則就開始追蹤
        if (distance <= minimunChaseDistance)
            return true;
        else
            return false;
    }

    void Patrol()
    {
        float distance = Vector3.Distance(myTransform.position, waypoints[waypoint].position);

        if (distance >= 1.0f)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, waypoints[waypoint].position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            waypoint++;
            if (waypoint >= waypoints.Count)
            {
                waypoint = 0;
            }
        }
    }
}