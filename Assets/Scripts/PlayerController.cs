using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("���a�򥻳]�w")]
    public float maxLifeValue;
    public float lifeValueChangeUnit;

    float lifeValue;

    // �w�q�ƥ�e�U
    public delegate void PlayerDamageEventHandler(float _damage);

    // �w�q�ƥ�
    public static event PlayerDamageEventHandler PlayerDamage;

    // Start is called before the first frame update
    void Start()
    {
        lifeValue = maxLifeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDamage(float _damage)
    {
        PlayerDamage?.Invoke(_damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lifeValue -= lifeValueChangeUnit;
            //Debug.Log(lifeAmount / maxLife);
            OnDamage(lifeValue / maxLifeValue);
        }
    }
}
