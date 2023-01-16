using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("參考物件")]
    public Camera fpsCam;
    public Transform attackPoint;

    [Header("子彈預置物件")]
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

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 在測試階段將射線設定為紅色線條，來看看線條長度夠不夠？

            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * 50, ForceMode.Impulse);
        }       
    }
}
