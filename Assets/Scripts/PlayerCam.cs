using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("鏡頭轉動敏感度")]
    public float sensX;   // 鏡頭X軸轉動敏感度
    public float sensY;   // 鏡頭Y軸轉動敏感度

    float xRotation;
    float yRotaiton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 鎖定滑鼠游標在畫面中央
        Cursor.visible = false;                     // 隱藏滑鼠游標
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;   // 取得滑鼠游標的X軸移動
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;   // 取得滑鼠游標的Y軸移動

        // 因為預設的XY軸移動方向在UNITY是反轉的，我們要將滑鼠X軸轉成物件的Y軸，Y軸轉成X軸
        xRotation -= mouseY; // 將滑鼠Y軸移動數值"倒轉"過來(正變負負變正)
        yRotaiton += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 30f); // 限定X軸轉動在正30度到負90度間(抬頭和低頭有限制角度)

        transform.rotation = Quaternion.Euler(xRotation, yRotaiton, 0); // 設定攝影機角度
    }
}
