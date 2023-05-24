using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";       // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;  // 設定最短的追蹤距離

    [Header("生命值")]
    public float maxLife = 10.0f;              // 設定敵人的最高生命值
    public Image lifeBarImage;                 // 設定敵人血條的圖片
    float lifeAmount;                          // 目前的生命值

    NavMeshAgent navMeshAgent;                 // 宣告NavMeshAgent物件
    GameObject targetObject = null;            // 目標物件的變數

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
        navMeshAgent = GetComponent<NavMeshAgent>();                   // 接收NavMeshAgent
        lifeAmount = maxLife;
    }

    void Update()
    {
        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // 判斷式：判斷距離是否低於最短追蹤距離，如果與目標的距離大於最小距離，就不追蹤，否則就開始追蹤
        if (distance <= minimunTraceDistance)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;

        faceTarget(); // 將敵人一直正面面對角色，因為敵人和角色位置會變化，所以要不斷Update

        // 判斷式：如果生命數值低於0，則讓敵人消失
        if (lifeAmount <= 0.0f)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(targetObject.transform.position);    // 讓自己往目標物的座標移動   
    }

    // 碰撞偵測
    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰到帶有Bullet標籤的物件，就要扣血，並且更新血條狀態
        if (collision.gameObject.tag == "Bullet")
        {
            lifeAmount -= 1.0f;
            //Debug.Log(lifeAmount / maxLife);
            lifeBarImage.fillAmount = lifeAmount/maxLife;
        }
    }

    // 函式：將敵人一直正面面對角色(也就是讓敵人的Z軸不斷的瞄準角色)
    void faceTarget()
    {
        Vector3 targetDir = targetObject.transform.position - transform.position;                               // 計算敵人與角色之間的向量
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.1f * Time.deltaTime, 0.0F);      // 依照敵人Z軸向量與兩者間向量，可以計算出需要旋轉的角度
        transform.rotation = Quaternion.LookRotation(newDir);                                                   // 進行旋轉
    }
}
