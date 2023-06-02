using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("���Y��ʱӷP��")]
    public float sensX;   // ���YX�b��ʱӷP��
    public float sensY;   // ���YY�b��ʱӷP��
    public float mouseSmoothTime = 0.03f;

    [HideInInspector] public float xRotation;
    [HideInInspector] public float yRotaiton;

    private Vector3 currentMouseDelta = Vector3.zero;
    private Vector3 currentMouseDeltaVelocity = Vector3.zero;

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

        // ��v�����ʧ@�L�աA�ϥ�SmoothDamp
        Vector3 targetMouseDelta = new Vector3(xRotation, yRotaiton, 0);
        currentMouseDelta = Vector3.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        transform.rotation = Quaternion.Euler(currentMouseDelta); // �]�w��v������
    }
}
