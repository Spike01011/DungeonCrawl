using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;


public class Dragon : MyEntity
{
    public ParticleSystem hitParticles;
    private Animator entityAnim;

    float MinDist = 1.8f;

    private bool isDie = false;

    private int isDieHash;
    private int isAttackHash;
    float timestamp;

    internal override void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        entityAnim = GetComponent<Animator>();
        isDieHash = Animator.StringToHash("isDie");
        isRunningHash = Animator.StringToHash("isWalk");
        isAttackHash = Animator.StringToHash("Attack");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();


        timestamp = Time.time + 1;
        baseDamage = 10f;
        baseAttackSpeed = 1f;
        baseHp = 70f;
        baseSpeed = 500f;

        speedMult = 1.0f;
        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        hpMult = 1.0f;
        hp = baseHp * hpMult;
    }


    internal override void FixedUpdate()
    {
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

    

    public void Die()
    {
        isRunning = false;
        isDie = true;
        entityAnim.SetBool(isDieHash, isDie);
        Player.GetComponent<PlayerController>().experience += experience * experienceMult;
        spawnManager.experience += experience * experienceMult;
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
    void OnTriggerEnter(Collider collider)
    {
        if (timestamp <= Time.time)
        {
            if (collider.CompareTag("Player"))
            {
                Instantiate(hitParticles, collider.transform.position, hitParticles.transform.rotation);
                collider.gameObject.GetComponent<PlayerController>().takeDamage(damage);
                timestamp = Time.time + (1 / attackSpeed);
            }
        }
    }


    internal void RotateTowardsPlayer()
    {
        transform.LookAt(Player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
