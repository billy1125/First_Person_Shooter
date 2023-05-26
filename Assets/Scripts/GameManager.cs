using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI�]�w")]
    public Image lifeBarImage;

    void OnEnable()
    {
        PlayerController.onHpChange += UpdateLifeBar;   // �q�\PlayerController��onHpChange�q��
                                                        // �]�N�O���A����ͩR���ܤơAGameManager���|���D�A�A�̾ڨ���ͩR�Ȫ��ƭȧ�s������e
                                                        // �o�˪��g�k�i�H��ֵ{���������̿�{��(��C�{�����X���{��)
    }

    void OnDisable()
    {
        PlayerController.onHpChange -= UpdateLifeBar;  // �����q�\PlayerController��onHpChange�q��
    }

    // �禡�G��s���
    private void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }
}
