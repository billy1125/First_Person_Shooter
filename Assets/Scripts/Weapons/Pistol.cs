using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Guns, IAttack
{
    // 方法：射擊武器
    public void Attack()
    {
        if (bulletsLeft > 0 && !isReloading)
        {
            Vector3 shootingDirection = AimToShoot();
            
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // 在攻擊點上面產生一個子彈
            currentBullet.transform.forward = shootingDirection; // 將子彈飛行方向與射線方向一致
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * fireForce, ForceMode.Impulse); // 依據飛行方向推送子彈

            bulletsLeft--;    // 將彈夾中的子彈減一，以下的寫法都是一樣的意思
                              // bulletsLeft -= 1;               
                              // bulletsLeft = bulletsLeft - 1;  // 比較囉嗦的寫法

            MakeRecoilForce(shootingDirection);

            onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");

            audioSource.Play();
        }
    } 
}
