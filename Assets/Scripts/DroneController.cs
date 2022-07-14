using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DroneController : MyEntity
{
    public GameObject laser;

    public GameObject laserPrefab;

    public GameObject specialPrefab;

    public Camera cam;

    private float range = 200f;

    private GameObject Player;
    private Rigidbody rb;

    float MinDist = 10f;

    // Start is called before the first frame update
    internal override void Start()
    {
        Player = GameObject.Find("Player");
        laser = GameObject.Find("laser");
        rb = GetComponent<Rigidbody>();

        baseDamage = 10f;
        baseAttackSpeed = 1f;
        baseHp = 200f;
        baseSpeed = 300f;

        speedMult = 1.0f;
        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        hpMult = 1.0f;
    }

    // Update is called once per frame
    internal override void FixedUpdate()
    {
        hp = baseHp * hpMult;
        speed = baseSpeed * speedMult;
        damage = baseDamage * damageMult;
        attackSpeed = baseAttackSpeed * attackSpeedMult;

        Move();
        Shoot();
    }

    void Shoot()
    {
       
    }

    internal override void Move()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= MinDist)
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.LookAt(Player.transform.position);
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }
    }
}
