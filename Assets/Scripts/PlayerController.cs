using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("玩家基本設定")]
    public float maxLifeValue;               // 設定玩家最高生命值
    public float lifeHpChangeUnit;           // 設定每次改變生命值的單位

    float lifeValue;

    public static Action<float> onHpChange;   // 設定一個動作，可以通知別的程式，我們設計一個改變生命值的通知動作

    void Start()
    {
        lifeValue = maxLifeValue;
    }

    // 碰撞偵測
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lifeValue -= lifeHpChangeUnit;
            onHpChange?.Invoke(lifeValue / maxLifeValue); // 告訴所有程式，生命值改變了
        }
    }
}
