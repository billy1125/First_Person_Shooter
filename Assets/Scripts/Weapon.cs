using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // �n���o�Ӥ~�౱���r��

public class Weapon : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("�l�u�w�m����")]
    public GameObject bullet;

    [Header("�j�K�]�w")]
    public bool isGun;
    public int magazineSize;        // �]�w�u���i�H��h�����l�u�H
    public int bulletsLeft;         // �l�u�٦��h�����H(�p�G�S���n���աA�A�i�H�]�w�� Private)
    public float reloadTime;        // �]�w���u���һݭn���ɶ�
    public float recoilForce;       // �ϧ@�ΤO

    bool reloading;             // ���L�ܼơG�x�s�O���O���b���u�������A�HTrue�G���b���u���BFalse�G���u�����ʧ@�w����

    [Header("UI����")]
    public TextMeshProUGUI ammunitionDisplay; // �u�q���
    public TextMeshProUGUI reloadingDisplay;  // ��ܬO���O���b���u���H

    private void Start()
    {
        bulletsLeft = magazineSize;        // �C���@�}�l�u���]�w���������A
        reloadingDisplay.enabled = false;  // �N��ܥ��b���u�����r�����ð_��

        ShowAmmoDisplay();                 // ��s�u�q���
    }

    // ��k�G�g���Z��
    public void Attack()
    {
        if (isGun && bulletsLeft > 0 && !reloading) { 
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

            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 30, ForceMode.Impulse); // �̾ڭ����V���e�l�u
            //currentBullet.GetComponent<Rigidbody>().AddForce(PlayerCamera.transform.up * , ForceMode.Impulse);

            bulletsLeft--;    // �N�u�������l�u��@�A�H�U���g�k���O�@�˪��N��
                              //bulletsLeft -= 1;               
                              //bulletsLeft = bulletsLeft - 1;  // ����o�۪��g�k

            // ��y�O����
            PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);                       
        }
        ShowAmmoDisplay();                 // ��s�u�q���
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");  // Ĳ�o�uFire�v��Ĳ�o�ܼ�
    }

    private void OnEnable()
    {
        ShowAmmoDisplay();                 // ��s�u�q���
    }

    // ��k�G���u��������ɶ��]�w
    public void Reload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;                      // �����N���u�����A�]�w���G���b���u��
            reloadingDisplay.enabled = true;       // �N���b���u�����r����ܥX��
            Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
        }
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
