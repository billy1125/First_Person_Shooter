using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //�ϥ�UnityEngine.AI

[System.Serializable]
public class ChaseState : IEnemyState
{
    [SerializeField] float chaseSpeed = 8;
    [SerializeField] float loseDistance = 5;
    [HideInInspector] public Transform target;
    [HideInInspector] Transform myTransform;
    [SerializeField] string targetName = "Player";

    GameObject player;
    NavMeshAgent navMeshAgent;

    public void OnEntry(Enemy enemy)
    {
        myTransform = enemy.transform;
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        player = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
    }

    public void OnUpdate(Enemy enemy)
    {
        if (PlayerLost())
        {
            enemy.ChangeState(enemy.patrolState);
        }
        else
        {
            Chase();
        }
    }

    public void OnExit(Enemy enemy)
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
            navMeshAgent.SetDestination(player.transform.position);    // ���ۤv���ؼЪ����y�в���   
        //myTransform.position = Vector3.MoveTowards(myTransform.position, target.position, chaseSpeed * Time.deltaTime);
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

        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(player.transform.position, myTransform.position);

        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ���A�p�G�P�ؼЪ��Z���j��̤p�Z���A�N���l�ܡA�_�h�N�}�l�l��
        if (distance > loseDistance)
            return true;
        else
            return false;
    }
}
