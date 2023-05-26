using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("���a�򥻳]�w")]
    public float maxLifeValue;               // �]�w���a�̰��ͩR��
    public float lifeHpChangeUnit;           // �]�w�C�����ܥͩR�Ȫ����

    float lifeValue;

    public static Action<float> onHpChange;   // �]�w�@�Ӱʧ@�A�i�H�q���O���{���A�ڭ̳]�p�@�ӧ��ܥͩR�Ȫ��q���ʧ@

    void Start()
    {
        lifeValue = maxLifeValue;
    }

    // �I������
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lifeValue -= lifeHpChangeUnit;
            onHpChange?.Invoke(lifeValue / maxLifeValue); // �i�D�Ҧ��{���A�ͩR�ȧ��ܤF
        }
    }
}
