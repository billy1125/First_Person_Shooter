using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IAttack
{
    public void Attack()
    {
        if (transform.GetChild(0).GetComponent<Animator>() != null)
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");  // Ĳ�o�uFire�v��Ĳ�o�ܼ�
    }
}
