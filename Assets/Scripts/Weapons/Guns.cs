using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour, IGunStatus, IReload
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

    private int _magazineSize;
    public int magazineSize { get { return _magazineSize; } set { _magazineSize = value; } }

    private bool _isReloading;
    public bool isReloading { get { return _isReloading; } }

    private int _bulletsLeft;
    public int bulletsLeft { get { return _bulletsLeft; } set { _bulletsLeft = value; } }

    void Start()
    {
        magazineSize = maxMagazineSize;
        bulletsLeft = magazineSize;        // 遊戲一開始彈夾設定為全滿狀態
    }
    
    // 方法：換彈夾的延遲時間設定
    public void Reload()
    {
        if (bulletsLeft < maxMagazineSize && !isReloading)
        {
            _isReloading = true;                      // 首先將換彈夾狀態設定為：正在換彈夾            
            Invoke("ReloadFinished", reloadTime);  // 依照reloadTime所設定的換彈夾時間倒數，時間為0時執行ReloadFinished方法
        }
    }

    // 方法：換彈夾
    private void ReloadFinished()
    {
        bulletsLeft = maxMagazineSize;          // 將子彈填滿
        _isReloading = false;                    // 將換彈夾狀態設定為：更換彈夾結束
        //reloadingDisplay.enabled = false;      // 將正在換彈夾的字幕隱藏，結束換彈夾的動作
    }

}
