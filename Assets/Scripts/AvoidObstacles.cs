using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacles : MonoBehaviour
{
    [Header("障礙物標籤")]
    public string detectionTag;

    [Header("角色設定")]
    public GameObject firstPerson;              // 角色的物件
    public float detectionDistance = 1.0f;      // 射線的偵測距離
    public float pushBackForce = 1.0f;          // 回推角色的力道設定

    void Update()
    {
        RaycastHit hit;
        Vector3 pos = transform.position;
        
        // 角色前方隨機左右45度的射線
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            Debug.DrawLine(pos, hit.point, Color.blue);
            PushPlayer(hit, detectionTag);
        }

        // 角色右側隨機45-90度的射線
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(45.0f, 90.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }

        // 角色左側隨機45-90度的射線
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, -90.0f), transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }

        // 角色前方隨機上下45度的射線
        if (Physics.Raycast(pos, Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), transform.right) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.red);
        }
    }

    // 函式：如果有偵測到障礙物，回推角色，避免角色整個黏過去
    void PushPlayer(RaycastHit _hit, string _detectionTag)
    {
        if (_hit.collider.tag == _detectionTag)
        {
            Vector3 targetDir = firstPerson.transform.position - _hit.point;
            firstPerson.GetComponent<Rigidbody>().AddForce(targetDir.normalized * pushBackForce, ForceMode.Impulse);
        }
    }
}

