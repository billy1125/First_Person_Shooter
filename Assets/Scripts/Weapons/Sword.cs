using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon, IAttack
{
    void OnEnable()
    {
        onUpdateWeaponStatus?.Invoke("Sword");
    }

    public void Attack()
    {
        if (transform.GetChild(0).GetComponent<Animator>() != null)
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");  // 觸發「Fire」的觸發變數
    }
}
