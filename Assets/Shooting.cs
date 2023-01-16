using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public Camera fpsCam;
    public Transform attackPoint;

    [Header("�l�u�w�m����")]
    public GameObject bullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view           

            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75); //Just a point far away from the player

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // �b���ն��q�N�g�u�]�w������u���A�Ӭݬݽu�����װ������H

            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * 50, ForceMode.Impulse);
        }       
    }
}
