using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacles : MonoBehaviour
{
    public string detectionTag;
    public GameObject FirstPerson;
    public float detectionDistance = 1.0f;
    public float pushBackForce = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 pos = transform.position;

        if (Physics.Raycast(pos, Quaternion.AngleAxis(0f, transform.up) * transform.forward, out hit, detectionDistance))
        {
            Debug.DrawLine(pos, hit.point, Color.blue);
            PushPlayer(hit, detectionTag);
        }
        if (Physics.Raycast(pos, Quaternion.AngleAxis(45f, transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }
        if (Physics.Raycast(pos, Quaternion.AngleAxis(-45f, transform.up) * transform.forward, out hit, detectionDistance))
        {
            PushPlayer(hit, detectionTag);
            Debug.DrawLine(pos, hit.point, Color.yellow);
        }
        //if (Physics.Raycast(pos, Quaternion.AngleAxis(90f, transform.up) * transform.forward, out hit, detectionDistance))
        //{            
        //    Debug.DrawLine(pos, hit.point, Color.red);
        //}
        //if (Physics.Raycast(pos, Quaternion.AngleAxis(-90f, transform.up) * transform.forward, out hit, detectionDistance))
        //{            
        //    Debug.DrawLine(pos, hit.point, Color.red);
        //}       
    }

    void PushPlayer(RaycastHit _hit, string _detectionTag)
    {
        if (_hit.collider.tag == _detectionTag)
        {
            Vector3 targetDir = FirstPerson.transform.position - _hit.point;
            FirstPerson.GetComponent<Rigidbody>().AddForce(targetDir.normalized * pushBackForce, ForceMode.Impulse);
        }
    }
}

