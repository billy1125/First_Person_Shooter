using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10); // �l�u�w�]�Q���|�۰ʧR���ۤv
    }

    // �I�������G�p�G�I��@�Ӫ���a���uTarget�v���ҡA�h�R���ۤv
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
            Destroy(gameObject);
    }
}
