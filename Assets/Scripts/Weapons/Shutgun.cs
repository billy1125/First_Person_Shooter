using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutgun : Guns, IAttack
{
    [Range(0.05f, 0.5f)] public float range;
    public int shutgunBullets;

    // 方法：射擊武器
    public void Attack()
    {
        if (bulletsLeft > 0 && !isReloading)
        {
            Vector3 shootingDirection = AimToShoot();

            for (int i = 1; i < shutgunBullets; i++)
            {
                GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
                // 將方向的數值亂數化，製造散彈效果
                Vector3 randomPoint = shootingDirection + new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
                currentBullet.transform.forward = randomPoint.normalized;
                currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * fireForce, ForceMode.Impulse); // 依據飛行方向推送子彈
                //Vector3 dir = PlayerCamera.transform.forward + new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
                //currentBullet.GetComponent<Rigidbody>().AddForce(dir * fireForce, ForceMode.Impulse);
            }
            
            bulletsLeft --;

            MakeRecoilForce(shootingDirection);
            
            onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");

            audioSource.Play();
        }
    }
}
