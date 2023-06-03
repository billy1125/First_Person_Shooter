using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackState : IEnemyState
{
    [Header("攻擊狀態設定")]
    [SerializeField] float minimunChaseDistance = 3;

    GameObject player;
    Transform myTransform;
    string targetName;

    public void OnEntry(Enemy enemy)
    {
        Debug.Log("AttackState");
        myTransform = enemy.transform;
        targetName = enemy.targetName;
        player = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
        enemy.Attack(true);
        enemy.animator.SetBool("Shooting", true);
    }

    public void OnUpdate(Enemy enemy)
    {
        //Debug.Log("Attacking");
        faceTarget();   // 將敵人一直正面面對角色，因為敵人和角色位置會變化，所以要不斷Update
      
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

    // 方法：將敵人一直正面面對角色(也就是讓敵人的Z軸不斷的瞄準角色)
    void faceTarget()
    {
        //Vector3 targetDir = player.transform.position - myTransform.position;                                   // 計算敵人與角色之間的向量
        //Vector3 newDir = Vector3.RotateTowards(myTransform.forward, targetDir, Time.deltaTime, 0.1f);           // 依照敵人Z軸向量與兩者間向量，可以計算出需要旋轉的角度
        //myTransform.rotation = Quaternion.LookRotation(newDir);                            // 進行旋轉
        Vector3 targetPos = new Vector3(player.transform.position.x, myTransform.position.y, player.transform.position.z);
        myTransform.LookAt(targetPos);
    }    

   

    private bool ChangeToChase()
    {
        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(player.transform.position, myTransform.position);

        // 判斷式：判斷距離是否低於最短追蹤距離，如果與目標的距離大於最小距離，就不追蹤，否則就開始追蹤
        if (distance >= minimunChaseDistance)
            return true;
        else
            return false;
    }  
}
