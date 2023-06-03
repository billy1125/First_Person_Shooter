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

    [HideInInspector] public string targetName;
    [HideInInspector] public float loseDistance;

    Transform myTransform;
    GameObject player;
    //RaycastHit hitInfo;
    
    public void OnEntry(EnemyBehavior enemy)
    {       
        myTransform = enemy.transform;
        player = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
    }

    public void OnUpdate(EnemyBehavior enemy)
    {
        Patrol();
        if (LookForPlayer())
        {
            //enemy.chaseState.SetTarget(targetName);
            enemy.ChangeState(enemy.chaseState);
        }
    }
    public void OnExit(EnemyBehavior enemy)
    {
        // This will be called when first entering the state
    }
    bool LookForPlayer()
    {
        //if (Physics.Raycast(myTransform.position, myTransform.forward, out hitInfo, 3))
        //{
        //    if (hitInfo.collider.tag == "Player")
        //    {
        //        return true;
        //    }
        //}

        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(player.transform.position, myTransform.position);
        //Debug.Log(distance);
        // 判斷式：判斷距離是否低於最短追蹤距離，如果與目標的距離大於最小距離，就不追蹤，否則就開始追蹤
        if (distance <= loseDistance)
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