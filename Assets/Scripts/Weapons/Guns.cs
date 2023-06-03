using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Guns : Weapon, IReload
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

    protected bool isReloading;
    protected int bulletsLeft;

    protected AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        bulletsLeft = maxMagazineSize;        // �C���@�}�l�u���]�w���������A
        onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");        
    }
    
    // ��k�G���u��������ɶ��]�w
    public void Reload()
    {
        if (bulletsLeft < maxMagazineSize && !isReloading)
        {
            isReloading = true;                      // �����N���u�����A�]�w���G���b���u��            
            Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
            onReload?.Invoke(true); 
        }
    }

    // ��k�G���u��
    private void ReloadFinished()
    {
        bulletsLeft = maxMagazineSize;          // �N�l�u��
        isReloading = false;                    // �N���u�����A�]�w���G�󴫼u������
        onReload?.Invoke(false);
        onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");
    }

    // ��k�G�˷�
    protected Vector3 AimToShoot()
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

        return shootingDirection.normalized;
    }

    // ��k�G��y�O����
    protected void MakeRecoilForce(Vector3 _dir)
    {     
        PlayerObejct.GetComponent<Rigidbody>().AddForce(_dir.normalized * -recoilForce, ForceMode.Impulse);
    }
}
