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

    [HideInInspector] public string targetName;
    [HideInInspector] public float loseDistance;

    Transform myTransform;
    GameObject player;
    //RaycastHit hitInfo;
    
    public void OnEntry(EnemyBehavior enemy)
    {       
        myTransform = enemy.transform;
        player = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
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

        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(player.transform.position, myTransform.position);
        //Debug.Log(distance);
        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ���A�p�G�P�ؼЪ��Z���j��̤p�Z���A�N���l�ܡA�_�h�N�}�l�l��
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