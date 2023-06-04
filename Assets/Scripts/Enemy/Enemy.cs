using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("�l�ܥؼг]�w")]
    public string targetName = "Player";       // �]�w�ؼЪ��󪺼��ҦW��
    public float minimunTraceDistance = 5.0f;  // �]�w�̵u���l�ܶZ��

    [Header("�ͩR��")]
    public float maxLife = 10.0f;              // �]�w�ĤH���̰��ͩR��
    public Image lifeBarImage;                 // �]�w�ĤH������Ϥ�
    public float lifeAmount;                          // �ثe���ͩR��

    [Header("�ĤH�����]�w")]
    public Transform attackPoint;
    public GameObject enemyBullet;
    public float fireForce;

    public Animator animator;

    protected AudioSource audioSource;

    public IEnemyState currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();

    void Start()
    {
        lifeAmount = maxLife;
        audioSource = GetComponent<AudioSource>();
        ChangeState(idleState);        
    }

    void Update()
    {
        currentState.OnUpdate(this);

        // �P�_���G�p�G�ͩR�ƭȧC��0�A�h���ĤH����
        if (lifeAmount <= 0.0f)
        {
            StopAllCoroutines();
            animator.SetTrigger("Dead");
            ChangeState(idleState);
            Destroy(gameObject, 10);
        }
    }

    void FixedUpdate()
    {
        //if (navMeshAgent.enabled)
        //    navMeshAgent.SetDestination(targetObject.transform.position);    // ���ۤv���ؼЪ����y�в���   
    }

    // �I������
    private void OnCollisionEnter(Collision collision)
    {
        // �p�G�I��a��Bullet���Ҫ�����A�N�n����A�åB��s������A
        if (collision.gameObject.tag == "Bullet")
        {
            lifeAmount -= 1.0f;
            //Debug.Log(lifeAmount / maxLife);
            lifeBarImage.fillAmount = lifeAmount / maxLife;
        }
    }

    // ��k�G���ܪ��A
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEntry(this);
    }

    // ��k�G����
    public void Attack(bool _enableFire)
    {
        if (_enableFire)
            StartCoroutine(FireWeapons());
        else
            StopAllCoroutines();
    }

    IEnumerator FireWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (enemyBullet != null)
            {
                GameObject bullet = Instantiate(enemyBullet, attackPoint.position, attackPoint.rotation);
                Vector3 dir = attackPoint.transform.forward;// + new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
                bullet.GetComponent<Rigidbody>().AddForce(dir * fireForce, ForceMode.Impulse);
                audioSource.Play();
            }
        }
    }
}
