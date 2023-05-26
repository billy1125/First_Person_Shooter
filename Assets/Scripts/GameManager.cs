using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI設定")]
    public Image lifeBarImage;

    void OnEnable()
    {
        PlayerController.onHpChange += UpdateLifeBar;   // 訂閱PlayerController的onHpChange通知
                                                        // 也就是說，角色生命值變化，GameManager都會知道，再依據角色生命值的數值更新血條內容
                                                        // 這樣的寫法可以減少程式之間的依賴程度(減低程式耦合的程度)
    }

    void OnDisable()
    {
        PlayerController.onHpChange -= UpdateLifeBar;  // 取消訂閱PlayerController的onHpChange通知
    }

    // 函式：更新血條
    private void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }
}
