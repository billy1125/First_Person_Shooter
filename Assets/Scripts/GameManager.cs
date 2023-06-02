using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // �n���o�Ӥ~�౱���r��

public class GameManager : MonoBehaviour
{
    [Header("UI�]�w")]
    public Image lifeBarImage;
    public TextMeshProUGUI ammunitionDisplay; // �u�q���
    public TextMeshProUGUI reloadingDisplay;  // ��ܬO���O���b���u���H

    void OnEnable()
    {
        PlayerController.onHpChange += UpdateLifeBar;   // �q�\PlayerController��onHpChange�q��
                                                        // �]�N�O���A����ͩR���ܤơAGameManager���|���D�A�A�̾ڨ���ͩR�Ȫ��ƭȧ�s������e
                                                        // �o�˪��g�k�i�H��ֵ{���������̿�{��(��C�{�����X���{��)
        Weapon.onUpdateWeaponStatus += ShowWeaponStatus;
        Weapon.onReload += ShowReloading;
    }

    void Start()
    {
        reloadingDisplay.enabled = false;
    }

    void OnDisable()
    {
        PlayerController.onHpChange -= UpdateLifeBar;  // �����q�\PlayerController��onHpChange�q��
        Weapon.onUpdateWeaponStatus -= ShowWeaponStatus;
        Weapon.onReload -= ShowReloading;
    }

    // �禡�G��s���
    private void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }

    void ShowReloading(bool _isReload)
    {
        reloadingDisplay.enabled = _isReload;
    }

    void ShowWeaponStatus(string _msg)
    {
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(_msg);
    }
}
