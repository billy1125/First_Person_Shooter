using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // �n���o�Ӥ~�౱���r��

public class Shooting : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("�l�u�w�m����")]
    public GameObject bullet;

    [Header("�j�K�]�w")]
    public int magazineSize;        // �]�w�u���i�H��h�����l�u�H
    public int bulletsLeft;         // �l�u�٦��h�����H(�p�G�S���n���աA�A�i�H�]�w�� Private)
    public float reloadTime;        // �]�w���u���һݭn���ɶ�
    public float recoilForce;       // �ϧ@�ΤO

    bool reloading;             // ���L�ܼơG�x�s�O���O���b���u�������A�HTrue�G���b���u���BFalse�G���u�����ʧ@�w����

    [Header("UI����")]
    public TextMeshProUGUI ammunitionDisplay; // �u�q���
    public TextMeshProUGUI reloadingDisplay;  // ��ܬO���O���b���u���H

    //bool allowInvoke = true;
    public Animator ani;

    private void Start()
    {        
        bulletsLeft = magazineSize;        // �C���@�}�l�u���]�w���������A
        reloadingDisplay.enabled = false;  // �N��ܥ��b���u�����r�����ð_��

        ShowAmmoDisplay();                 // ��s�u�q���
    }

    private void Update()
    {
        MyInput();
    }

    // ��k�G�������a�ާ@���A
    private void MyInput()
    {
        // �P�_�G���S�����U����H
        if (Input.GetMouseButtonDown(0) == true)
        {
            // �p�G�٦��l�u�A�åB�S�����b���ˤl�u�A�N�i�H�g��
            if (bulletsLeft > 0 && !reloading)
            {
                Shoot();
            }
        }

        // �P�_�G1.�����UR��B2.�l�u�ƶq�C��u�������u�q�B3.���O���u�������A�A�T�ӱ��󳣺����A�N�i�H���u��
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();
    }

    // ��k�G�g���Z��
    private void Shoot()
    {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // �q��v���g�X�@���g�u
        RaycastHit hit;  // �ŧi�@�Ӯg���I
        Vector3 targetPoint;  // �ŧi�@�Ӧ�m�I�ܼơA��ɭԦp�G������F��A�N�s��o���ܼ�

        // �p�G�g�u�������ƸI���骺����
        if (Physics.Raycast(ray, out hit) == true)
            targetPoint = hit.point;         // �N���쪫�󪺦�m�I�s�i targetPoint
        else
            targetPoint = ray.GetPoint(75);  // �p�G�S�����쪫��A�N�H����75�������I���o�@���I�A�s�i targetPoint

        Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // �e�X�o���g�u

        Vector3 shootingDirection = targetPoint - attackPoint.position; // �H�_�I�P���I�������I��m�A�p��X�g�u����V
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // �b�����I�W�����ͤ@�Ӥl�u
        currentBullet.transform.forward = shootingDirection.normalized; // �N�l�u�����V�P�g�u��V�@�P

        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 20, ForceMode.Impulse); // �̾ڭ����V���e�l�u
        //currentBullet.GetComponent<Rigidbody>().AddForce(PlayerCamera.transform.up * , ForceMode.Impulse);

        bulletsLeft--;    // �N�u�������l�u��@�A�H�U���g�k���O�@�˪��N��
                          //bulletsLeft -= 1;               
                          //bulletsLeft = bulletsLeft - 1;  // ����o�۪��g�k

        // ��y�O����
        this.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);

        ShowAmmoDisplay();                 // ��s�u�q���

        ani.SetTrigger("Fire");
    }

    // ��k�G���u��������ɶ��]�w
    private void Reload()
    {
        reloading = true;                      // �����N���u�����A�]�w���G���b���u��
        reloadingDisplay.enabled = true;       // �N���b���u�����r����ܥX��
        Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
    }

    // ��k�G���u��
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;            // �N�l�u��
        reloading = false;                     // �N���u�����A�]�w���G�󴫼u������
        reloadingDisplay.enabled = false;      // �N���b���u�����r�����áA�������u�����ʧ@
        ShowAmmoDisplay();
    }

    // ��k�G��s�u�q���
    private void ShowAmmoDisplay()
    {        
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText($"Ammo {bulletsLeft} / {magazineSize}");
    }
}
