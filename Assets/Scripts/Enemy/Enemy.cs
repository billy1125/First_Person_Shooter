using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";       // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;  // 設定最短的追蹤距離

    [Header("生命值")]
    public float maxLife = 10.0f;              // 設定敵人的最高生命值
    public Image lifeBarImage;                 // 設定敵人血條的圖片
    public float lifeAmount;                          // 目前的生命值

    [Header("敵人攻擊設定")]
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

        // 判斷式：如果生命數值低於0，則讓敵人消失
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
        //    navMeshAgent.SetDestination(targetObject.transform.position);    // 讓自己往目標物的座標移動   
    }

    // 碰撞偵測
    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰到帶有Bullet標籤的物件，就要扣血，並且更新血條狀態
        if (collision.gameObject.tag == "Bullet")
        {
            lifeAmount -= 1.0f;
            //Debug.Log(lifeAmount / maxLife);
            lifeBarImage.fillAmount = lifeAmount / maxLife;
        }
    }

    // 方法：改變狀態
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEntry(this);
    }

    // 方法：攻擊
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
