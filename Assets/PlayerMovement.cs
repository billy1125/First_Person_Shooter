using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed;
    public float jumpForce;          // 跳躍力道
    public float jumpCooldown;       // 設定要幾秒後才能向上跳躍
    public float groundDrag;
    public float airMultiplier;

    [Header("按鍵綁定")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("基本設定")]
    public Transform PlayerCamera;   // 攝影機

    [Header("地板確認")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    private bool readyToJump;        // 設定是否可以跳躍
    private float horizontalInput;   // 左右方向按鍵的數值(-1 <= X <= +1)
    private float verticalInput;     // 上下方向按鍵的數值(-1 <= Y <= +1)

    private Vector3 moveDirection;   // 移動方向

    private Rigidbody rbFirstPerson; // 第一人稱物件(膠囊體)的剛體

    private void Start()
    {
        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;         // 鎖定第一人稱物件剛體旋轉，不讓膠囊體因為碰到物件就亂轉
        readyToJump = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();   // 偵測速度，過快就減速

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawRay(transform.position, new Vector3(0, -(playerHeight * 0.5f + 0.3f) , 0), Color.red);
        // handle drag
        if (grounded)
            rbFirstPerson.drag = groundDrag;
        else
            rbFirstPerson.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();     // 只要是物件移動，建議你放到FixedUpdate()        
    }

    // 方法：取得目前玩家按方向鍵上下左右的數值，控制跳躍行為
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // 如果按下設定的跳躍按鍵
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // 如果跳躍過後，就會依照設定的限制時間倒數，時間到了才能往上跳躍
        }
    }

    private void MovePlayer()
    {
        // 計算移動方向(其實就是計算X軸與Z軸兩個方向的力量)
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;
        // 推動第一人稱物件
        // on ground
        if (grounded)
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    // 方法：偵測速度並減速
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z); // 取得僅X軸與Z軸的平面速度
   
        // 如果平面速度大於預設速度值，就將物件的速度限定於預設速度值
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }
    }

    // 方法：跳躍
    private void Jump()
    {
        // 重新設定Y軸速度
        rbFirstPerson.velocity = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
        // 由下往上推第一人稱物件，ForceMode.Impulse可以讓推送的模式為一瞬間，會更像跳躍的感覺
        rbFirstPerson.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // 方法：重新設定變數readyToJump為true的方法
    private void ResetJump()
    {
        readyToJump = true;
    }
}