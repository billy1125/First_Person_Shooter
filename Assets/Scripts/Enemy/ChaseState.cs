using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //�ϥ�UnityEngine.AI

[System.Serializable]
public class ChaseState : IEnemyState
{
    [Header("�l�����A�]�w")]
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
        player = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
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
            navMeshAgent.SetDestination(player.transform.position);    // ���ۤv���ؼЪ����y�в���   
        //myTransform.position = Vector3.MoveTowards(myTransform.position, target.position, chaseSpeed * Time.deltaTime);
    }

    void BackToPatrolArea()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(patrolPoints.position);    // ���ۤv���ؼЪ����y�в���  
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
