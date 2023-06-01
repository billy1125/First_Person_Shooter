using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutgun : Guns, IAttack
{
    // ��k�G�g���Z��
    public void Attack()
    {
        if (bulletsLeft > 0 && !isReloading)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // �q��v���g�X�@���g�u
            RaycastHit hit;  // �ŧi�@�Ӯg���I
            Vector3 targetPoint;  // �ŧi�@�Ӧ�m�I�ܼơA��ɭԦp�G������F��A�N�s��o���ܼ�

            // �p�G�g�u�������ƸI���骺����
            if (Physics.Raycast(ray, out hit) == true)
                targetPoint = hit.point;         // �N���쪫�󪺦�m�I�s�i targetPoint
            else
                targetPoint = ray.GetPoint(75);  // �p�G�S�����쪫��A�N�H����75�������I���o�@���I�A�s�i targetPoint

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // �e�X�o���g�u

            Vector3 shootingDirection = targetPoint - attackPoint.position; // �H�_�I�P���I�������I��m�A�p��X�g�u����V
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // �b�����I�W�����ͤ@�Ӥl�u
            currentBullet.transform.forward = shootingDirection.normalized; // �N�l�u�����V�P�g�u��V�@�P

            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * fireForce, ForceMode.Impulse); // �̾ڭ����V���e�l�u
            //currentBullet.GetComponent<Rigidbody>().AddForce(PlayerCamera.transform.up * , ForceMode.Impulse);

            bulletsLeft--;    // �N�u�������l�u��@�A�H�U���g�k���O�@�˪��N��
                              //bulletsLeft -= 1;               
                              //bulletsLeft = bulletsLeft - 1;  // ����o�۪��g�k

            // ��y�O����
            PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);
        }
    }
}
