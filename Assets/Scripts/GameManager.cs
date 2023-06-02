using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // 要有這個才能控制文字框

public class GameManager : MonoBehaviour
{
    [Header("UI設定")]
    public Image lifeBarImage;
    public TextMeshProUGUI ammunitionDisplay; // 彈量顯示
    public TextMeshProUGUI reloadingDisplay;  // 顯示是不是正在換彈夾？

    void OnEnable()
    {
        PlayerController.onHpChange += UpdateLifeBar;   // 訂閱PlayerController的onHpChange通知
                                                        // 也就是說，角色生命值變化，GameManager都會知道，再依據角色生命值的數值更新血條內容
                                                        // 這樣的寫法可以減少程式之間的依賴程度(減低程式耦合的程度)
        Weapon.onUpdateWeaponStatus += ShowWeaponStatus;
        Weapon.onReload += ShowReloading;
    }

    void Start()
    {
        reloadingDisplay.enabled = false;
    }

    void OnDisable()
    {
        PlayerController.onHpChange -= UpdateLifeBar;  // 取消訂閱PlayerController的onHpChange通知
        Weapon.onUpdateWeaponStatus -= ShowWeaponStatus;
        Weapon.onReload -= ShowReloading;
    }

    // 函式：更新血條
    private void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }

    void ShowReloading(bool _isReload)
    {
        reloadingDisplay.enabled = _isReload;
    }

    void ShowWeaponStatus(string _msg)
    {
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(_msg);
    }
}
