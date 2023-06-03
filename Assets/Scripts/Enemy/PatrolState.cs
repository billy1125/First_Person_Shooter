using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolState : IEnemyState
{
    [Header("���ު��A�]�w")]
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
        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(player.transform.position, myTransform.position);
        //Debug.Log(distance);
        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ���A�p�G�P�ؼЪ��Z���j��̤p�Z���A�N���l�ܡA�_�h�N�}�l�l��
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