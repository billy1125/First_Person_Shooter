using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackState : IEnemyState
{
    [Header("�������A�]�w")]
    [SerializeField] float minimunChaseDistance = 3;

    GameObject player;
    Transform myTransform;
    string targetName;

    public void OnEntry(Enemy enemy)
    {
        Debug.Log("AttackState");
        myTransform = enemy.transform;
        targetName = enemy.targetName;
        player = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
        enemy.Attack(true);
        enemy.animator.SetBool("Shooting", true);
    }

    public void OnUpdate(Enemy enemy)
    {
        //Debug.Log("Attacking");
        faceTarget();   // �N�ĤH�@���������﨤��A�]���ĤH�M�����m�|�ܤơA�ҥH�n���_Update
      
        if (ChangeToChase())
        {
            enemy.animator.SetBool("Shooting", false);
            enemy.ChangeState(enemy.chaseState);
        }            
    }

    public void OnExit(Enemy enemy)
    {
        enemy.Attack(false);
    }

    // ��k�G�N�ĤH�@���������﨤��(�]�N�O���ĤH��Z�b���_���˷Ǩ���)
    void faceTarget()
    {
        //Vector3 targetDir = player.transform.position - myTransform.position;                                   // �p��ĤH�P���⤧�����V�q
        //Vector3 newDir = Vector3.RotateTowards(myTransform.forward, targetDir, Time.deltaTime, 0.1f);           // �̷ӼĤHZ�b�V�q�P��̶��V�q�A�i�H�p��X�ݭn���઺����
        //myTransform.rotation = Quaternion.LookRotation(newDir);                            // �i�����
        Vector3 targetPos = new Vector3(player.transform.position.x, myTransform.position.y, player.transform.position.z);
        myTransform.LookAt(targetPos);
    }    

   

    private bool ChangeToChase()
    {
        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(player.transform.position, myTransform.position);

        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ���A�p�G�P�ؼЪ��Z���j��̤p�Z���A�N���l�ܡA�_�h�N�}�l�l��
        if (distance >= minimunChaseDistance)
            return true;
        else
            return false;
    }  
}
