using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Dragon : MyEntity
{
    private Animator entityAnim;
    public GameObject Player;
    private Rigidbody rb;

    float MinDist = 1.8f;

    private bool isDie = false;

    private int isDieHash;
    private int isAttackHash;


    internal override void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        entityAnim = GetComponent<Animator>();
        isDieHash = Animator.StringToHash("isDie");
        isRunningHash = Animator.StringToHash("isWalk");
        isAttackHash = Animator.StringToHash("Attack");

        baseDamage = 10f;
        baseAttackSpeed = 1f;
        baseHp = 200f;
        baseSpeed = 600f;

        speedMult = 1.0f;
        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        hpMult = 1.0f;
    }


    internal override void FixedUpdate()
    {
        hp = baseHp * hpMult;
        speed = baseSpeed * speedMult;
        damage = baseDamage * damageMult;
        attackSpeed = baseAttackSpeed * attackSpeedMult;

        RotateTowardsPlayer();

        if (Vector3.Distance(transform.position, Player.transform.position) <= MinDist)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    
    public void takeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        isRunning = false;
        isDie = true;
        entityAnim.SetBool(isDieHash, isDie);
        Destroy(transform.gameObject);

    }

    internal override void Move()
    {
        isRunning = true;
        entityAnim.SetBool(isRunningHash, isRunning);
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    internal void Attack()
    {
        isRunning = false;
        entityAnim.SetBool(isRunningHash, isRunning);
        entityAnim.SetTrigger(isAttackHash);
    }

    internal void RotateTowardsPlayer()
    {
        transform.LookAt(Player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
