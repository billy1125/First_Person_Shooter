using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("���ʳ]�w")]
    public float moveSpeed;
    public float jumpForce;          // ���D�O�D
    public float jumpCooldown;       // �]�w�n�X���~��V�W���D
    public float groundDrag;         // �a������t
    public float airMultiplier;      // �b�Ť����[���t�סA�p�G�]�w��0�N�N���୸�A��ĳ�o�ӭȭn�p��1

    [Header("����j�w")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("�򥻳]�w")]
    public Transform PlayerCamera;   // ��v��

    [Header("�a�O�T�{")]
    public float playerHeight;       // �]�w���a����
    public LayerMask whatIsGround;   // �]�w���@�ӹϼh�O�g�u�i�H���쪺
    public bool grounded;            // ���L�ܼơG���S������a��

    private bool readyToJump;        // �]�w�O�_�i�H���D
    private float horizontalInput;   // ���k��V���䪺�ƭ�(-1 <= X <= +1)
    private float verticalInput;     // �W�U��V���䪺�ƭ�(-1 <= Y <= +1)

    private Vector3 moveDirection;   // ���ʤ�V

    private Rigidbody rbFirstPerson; // �Ĥ@�H�٪���(���n��)������

    private void Start()
    {
        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;         // ��w�Ĥ@�H�٪���������A�������n��]���I�쪫��N����
        readyToJump = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();   // �����t�סA�L�ִN��t

        // �g�X�@���ݤ��쪺�g�u�A�ӧP�_���S������a���H
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawRay(transform.position, new Vector3(0, -(playerHeight * 0.5f + 0.3f), 0), Color.red); // �b���ն��q�N�g�u�]�w������u���A�Ӭݬݽu�����װ������H
        // �p�G�I��a�O�A�N�]�w�@�Ӥϧ@�ΤO(�o�ӥi�H�s�y�H�����ʪ���t�P)
        if (grounded)
            rbFirstPerson.drag = groundDrag;
        else
            rbFirstPerson.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();     // �u�n�O���󲾰ʡA��ĳ�A���FixedUpdate()        
    }

    // ��k�G���o�ثe���a����V��W�U���k���ƭȡA������D�欰
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // �p�G���U�]�w�����D����
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // �p�G���D�L��A�N�|�̷ӳ]�w������ɶ��˼ơA�ɶ���F�~�੹�W���D
        }
    }

    private void MovePlayer()
    {
        // �p�Ⲿ�ʤ�V(���N�O�p��X�b�PZ�b��Ӥ�V���O�q)
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;
       
        // �p�G�b�a���A���ʤ覡�����q����
        if (grounded)
        {
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }            
        // �p�G���b�a���A�h���ʳt���٥i�H���W�@�Ӧb�Ť����[���Ʀr�A�i�H�s�y�H���@�����W�����W�H����ĪG
        else if (!grounded)
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    // ��k�G�����t�רô�t
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z); // ���o��X�b�PZ�b�������t��

        // �p�G�����t�פj��w�]�t�׭ȡA�N�N���󪺳t�׭��w��w�]�t�׭�
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }
    }

    // ��k�G���D
    private void Jump()
    {
        // ���s�]�wY�b�t��
        rbFirstPerson.velocity = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
        // �ѤU���W���Ĥ@�H�٪���AForceMode.Impulse�i�H�����e���Ҧ����@�����A�|�󹳸��D���Pı
        rbFirstPerson.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // ��k�G���s�]�w�ܼ�readyToJump��true����k
    private void ResetJump()
    {
        readyToJump = true;
    }
}