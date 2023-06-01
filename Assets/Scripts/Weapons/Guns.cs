using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour, IGunStatus, IReload
{
    [Header("�ѦҪ���")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("�l�u�w�m����")]
    [SerializeField] protected GameObject bullet;

    [Header("�j�K�]�w")]
    public int maxMagazineSize;     // �]�w�u���i�H��h�����l�u�H
    public float reloadTime;        // �]�w���u���һݭn���ɶ�
    public float fireForce;         // �l�u�g���O�D
    public float recoilForce;       // �ϧ@�ΤO

    private int _magazineSize;
    public int magazineSize { get { return _magazineSize; } set { _magazineSize = value; } }

    private bool _isReloading;
    public bool isReloading { get { return _isReloading; } }

    private int _bulletsLeft;
    public int bulletsLeft { get { return _bulletsLeft; } set { _bulletsLeft = value; } }

    void Start()
    {
        magazineSize = maxMagazineSize;
        bulletsLeft = magazineSize;        // �C���@�}�l�u���]�w���������A
    }
    
    // ��k�G���u��������ɶ��]�w
    public void Reload()
    {
        if (bulletsLeft < maxMagazineSize && !isReloading)
        {
            _isReloading = true;                      // �����N���u�����A�]�w���G���b���u��            
            Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
        }
    }

    // ��k�G���u��
    private void ReloadFinished()
    {
        bulletsLeft = maxMagazineSize;          // �N�l�u��
        _isReloading = false;                    // �N���u�����A�]�w���G�󴫼u������
        //reloadingDisplay.enabled = false;      // �N���b���u�����r�����áA�������u�����ʧ@
    }

}
