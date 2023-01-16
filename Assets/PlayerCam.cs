using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("���Y��ʱӷP��")]
    public float sensX;   // ���YX�b��ʱӷP��
    public float sensY;   // ���YY�b��ʱӷP��

    float xRotation;
    float yRotaiton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // ��w�ƹ���Цb�e������
        Cursor.visible = false;                     // ���÷ƹ����
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;   // ���o�ƹ���Ъ�X�b����
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;   // ���o�ƹ���Ъ�Y�b����

        // �]���w�]��XY�b���ʤ�V�bUNITY�O���઺�A�ڭ̭n�N�ƹ�X�b�ন����Y�b�AY�b�নX�b
        xRotation -= mouseY; // �N�ƹ�Y�b���ʼƭ�"����"�L��(���ܭt�t�ܥ�)
        yRotaiton += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 30f); // ���wX�b��ʦb��30�ר�t90�׶�(���Y�M�C�Y�������)

        transform.rotation = Quaternion.Euler(xRotation, yRotaiton, 0); // �]�w��v������
    }
}
