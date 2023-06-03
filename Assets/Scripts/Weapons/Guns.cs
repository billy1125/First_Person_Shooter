using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Guns : Weapon, IReload
{
    [Header("參考物件")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("子彈預置物件")]
    [SerializeField] protected GameObject bullet;

    [Header("槍枝設定")]
    public int maxMagazineSize;     // 設定彈夾可以放多少顆子彈？
    public float reloadTime;        // 設定換彈夾所需要的時間
    public float fireForce;         // 子彈射擊力道
    public float recoilForce;       // 反作用力

    protected bool isReloading;
    protected int bulletsLeft;

    protected AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        bulletsLeft = maxMagazineSize;        // 遊戲一開始彈夾設定為全滿狀態
        onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");        
    }
    
    // 方法：換彈夾的延遲時間設定
    public void Reload()
    {
        if (bulletsLeft < maxMagazineSize && !isReloading)
        {
            isReloading = true;                      // 首先將換彈夾狀態設定為：正在換彈夾            
            Invoke("ReloadFinished", reloadTime);  // 依照reloadTime所設定的換彈夾時間倒數，時間為0時執行ReloadFinished方法
            onReload?.Invoke(true); 
        }
    }

    // 方法：換彈夾
    private void ReloadFinished()
    {
        bulletsLeft = maxMagazineSize;          // 將子彈填滿
        isReloading = false;                    // 將換彈夾狀態設定為：更換彈夾結束
        onReload?.Invoke(false);
        onUpdateWeaponStatus?.Invoke($"Ammo {bulletsLeft} / {maxMagazineSize}");
    }

    // 方法：瞄準
    protected Vector3 AimToShoot()
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

        return shootingDirection.normalized;
    }

    // 方法：後座力模擬
    protected void MakeRecoilForce(Vector3 _dir)
    {     
        PlayerObejct.GetComponent<Rigidbody>().AddForce(_dir.normalized * -recoilForce, ForceMode.Impulse);
    }
}
