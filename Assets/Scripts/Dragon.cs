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

    public Dragon()
    {
    baseDamage = 10f;
    baseAttackSpeed = 1f;
    baseHp = 200f;
    baseSpeed = 10000f;
    }

    internal override void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        entityAnim = GetComponent<Animator>();
        isDieHash = Animator.StringToHash("isDie");
        isRunningHash = Animator.StringToHash("isWalk");
        isAttackHash = Animator.StringToHash("Attack");
    }


    internal override void FixedUpdate()
    {
        hp = baseHp * percentageMaxHpBonus;
        speed = baseSpeed * movementSpeedMulti;
        damage = baseDamage * damageMult;
        attackSpeed = baseAttackSpeed * attackSpeedMulti;

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
        rb.velocity = transform.forward * Time.deltaTime * speed/20;
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
