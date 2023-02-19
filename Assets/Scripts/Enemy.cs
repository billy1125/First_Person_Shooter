using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";                     // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;                // 設定最短的追蹤距離
    
    GameObject targetObject = null;                          // 目標物件的變數
    bool enableMove = false;                                 // 如果目標物件距離夠短，這個變數為true

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
    }

    void Update()
    {
        // 計算目標物件和敵人之間的距離
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // 判斷式：判斷距離是否低於最短追蹤距離
        if (distance <= minimunTraceDistance)
            enableMove = true;
        else
            enableMove = false;
    }

    void FixedUpdate()
    {
        // enableMove為true，就去追蹤目標
        if (enableMove)
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, Time.deltaTime); // 讓紅方塊往目標物的座標平滑移動                                                                                                           
    }
}
