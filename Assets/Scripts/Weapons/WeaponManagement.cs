using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    [Header("武器")]
    public GameObject[] weaponObjects;        // 武器清單

    int weaponNumber = 0;                     // 目前選擇武器的順序編號
    GameObject weaponInUse;                   // 目前選擇武器

    private void Start()
    {
        weaponInUse = weaponObjects[0];    // 遊戲一開始設定武器為第0個武器
    }

    private void Update()
    {
        MyInput();
    }

    // 方法：偵測玩家操作狀態
    private void MyInput()
    {
        // 判斷：有沒有按下左鍵？
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (weaponInUse.TryGetComponent(out IAttack attackObj))
                attackObj.Attack();
            // 如果還有子彈，並且沒有正在重裝子彈，就可以射擊
            //weaponInUse.GetComponent<Weapon>().Attack();
            //IAttack attackObj = weaponInUse.GetComponent<IAttack>();
            //if (attackObj != null)
            //    attackObj.Attack();
        }

        // 判斷：1.有按下R鍵、2.子彈數量低於彈夾內的彈量、3.不是換彈夾的狀態，三個條件都滿足，就可以換彈夾
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (weaponInUse.TryGetComponent(out IReload reloadObj))
                reloadObj.Reload();
        }            

        // 判斷：按下數字鍵1，切換為武器0
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        // 判斷：按下數字鍵2，切換為武器1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);

        // 判斷：滾動滑鼠滾輪
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)      // 往前滾動
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // 往後滾動
            SwitchWeapon(-1);        
    }
    
    // 方法：武器切換，參數_addNumber、_weaponNumber
    private void SwitchWeapon(int _addNumber, int _weaponNumber = 0)
    {
        // 將武器清單全部隱藏，先一次全部隱藏，再顯示需要的武器
        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        // switch 判斷式：以參數_addNumber判斷要怎麼切換武器
        switch (_addNumber)
        {
            case 0:                                                   // _addNumber == 0，代表用按鍵直接指定武器陣列位址
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                   // _addNumber == 1，代表往上滾滑鼠滾輪
                if (weaponNumber == weaponObjects.Length - 1)         // 實現循環數字，假定原本的武器陣列位址已經是最後一個武器，則將武器陣列位址設定為0
                    weaponNumber = 0;
                else
                    weaponNumber += 1;
                //weaponNumber = (weaponNumber == weaponObjects.Length - 1) ? 0 : weaponNumber += 1; // 也可以把以上的判斷式寫成這樣
                break;
            case -1:                                                   // _addNumber == -1，代表往下滾滑鼠滾輪
                if (weaponNumber == 0)                                 // 實現循環數字，假定原本的武器陣列位址是第一個武器，則將武器陣列位址為清單的最後一個位址
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                //weaponNumber = (weaponNumber == 0) ? weaponObjects.Length - 1 : weaponNumber -= 1; // 也可以把以上的判斷式寫成這樣
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);    // 顯示所指定的武器
        weaponInUse = weaponObjects[weaponNumber];      // 設定目前所選擇的武器物件(屆時可以用來執行武器所特定的方法，下一章節會介紹)
    }    
}
