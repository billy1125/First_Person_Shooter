using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutgun : Guns, IAttack
{
    // 方法：射擊武器
    public void Attack()
    {
        if (bulletsLeft > 0 && !isReloading)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // 從攝影機射出一條射線
            RaycastHit hit;  // 宣告一個射擊點
            Vector3 targetPoint;  // 宣告一個位置點變數，到時候如果有打到東西，就存到這個變數

            // 如果射線有打到具備碰撞體的物件
            if (Physics.Raycast(ray, out hit) == true)
                targetPoint = hit.point;         // 將打到物件的位置點存進 targetPoint
            else
                targetPoint = ray.GetPoint(75);  // 如果沒有打到物件，就以長度75的末端點取得一個點，存進 targetPoint

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 畫出這條射線

            Vector3 shootingDirection = targetPoint - attackPoint.position; // 以起點與終點之間兩點位置，計算出射線的方向
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // 在攻擊點上面產生一個子彈
            currentBullet.transform.forward = shootingDirection.normalized; // 將子彈飛行方向與射線方向一致

            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * fireForce, ForceMode.Impulse); // 依據飛行方向推送子彈
            //currentBullet.GetComponent<Rigidbody>().AddForce(PlayerCamera.transform.up * , ForceMode.Impulse);

            bulletsLeft--;    // 將彈夾中的子彈減一，以下的寫法都是一樣的意思
                              //bulletsLeft -= 1;               
                              //bulletsLeft = bulletsLeft - 1;  // 比較囉嗦的寫法

            // 後座力模擬
            PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);
        }
    }
}
