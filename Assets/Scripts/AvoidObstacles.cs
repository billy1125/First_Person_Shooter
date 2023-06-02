using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacles : MonoBehaviour
{
    [Header("��ê������")]
    public string detectionTag;

    [Header("����]�w")]
    public GameObject firstPerson;              // ���⪺����
    public float detectionDistance = 1.0f;      // �g�u�������Z��
    public float pushBackForce = 1.0f;          // �^�����⪺�O�D�]�w

    void Update()
    {
        RaycastHit hit;
        Vector3 pos = transform.position;
        
        // ����e���H�����k45�ת��g�u
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            Debug.DrawLine(pos, hit.point, Color.blue);
            PushPlayer(hit, detectionTag);
        }

        // ����k���H��45-90�ת��g�u
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(45.0f, 90.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }

        // ���⥪���H��45-90�ת��g�u
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, -90.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }

        // ����e���H���W�U45�ת��g�u
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), transform.right) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.red);
        }
    }

    // �禡�G�p�G���������ê���A�^������A�קK�������H�L�h
    void PushPlayer(RaycastHit _hit, string _detectionTag)
    {
        if (_hit.collider.tag == _detectionTag)
        {
            Vector3 targetDir = firstPerson.transform.position - _hit.point;
            firstPerson.GetComponent<Rigidbody>().AddForce(targetDir.normalized * pushBackForce, ForceMode.Impulse);
        }
    }
}

