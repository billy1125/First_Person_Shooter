using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Guns, IAttack
{
    // ��k�G�g���Z��
    public void Attack()
    {
        if (bulletsLeft > 0 && !isReloading)
        {
            Vector3 shootingDirection = AimToShoot();
            
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // �b�����I�W�����ͤ@�Ӥl�u
            currentBullet.transform.forward = shootingDirection; // �N�l�u�����V�P�g�u��V�@�P
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * fireForce, ForceMode.Impulse); // �̾ڭ����V���e�l�u

            bulletsLeft--;    // �N�u�������l�u��@�A�H�U���g�k���O�@�˪��N��
                              // bulletsLeft -= 1;               
                              // bulletsLeft = bulletsLeft - 1;  // ����o�۪��g�k

            MakeRecoilForce(shootingDirection);

            onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");

            audioSource.Play();
        }
    } 
}
