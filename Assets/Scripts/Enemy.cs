using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI 

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";       // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;  // 設定最短的追蹤距離

    NavMeshAgent navMeshAgent;                 // 宣告NavMeshAgent物件
    public GameObject targetObject = null;            // 目標物件的變數

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
        navMeshAgent = GetComponent<NavMeshAgent>();                   // 接收NavMeshAgent
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
    }

    void FixedUpdate()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(targetObject.transform.position);    // 讓自己往目標物的座標移動   
    }
}
