using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDrone : MyEntity
{
    public GameObject laserPrefab;  
    private PlayerController playerController;

    private float range = 200f;
    internal float currentShotDamage;
    float MinDist = 10f;
    float ShootDistance = 40f;
    public Vector3 route;
    int counter = 0;




    // Start is called before the first frame update
    internal override void Start()
    {
        Player = GameObject.Find("Player");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerController = Player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();

        baseDamage = 10f;
        baseAttackSpeed = 1f;
        baseHp = 30f;
        baseSpeed = 200f;

        speedMult = 1.0f;
        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        hpMult = 1.0f;

        hp = baseHp * hpMult;

        InvokeRepeating("Shoot", 1, 1);

        transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }

    internal override void FixedUpdate()
    {
        speed = baseSpeed * speedMult;
        damage = baseDamage * damageMult;
        attackSpeed = baseAttackSpeed * attackSpeedMult;
        Move();
    }

    void Shoot()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= ShootDistance)
        {
            var currentLaser = Instantiate(laserPrefab, transform.position, transform.rotation);
            currentLaser.transform.LookAt(Player.transform.position);
            currentLaser.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            var currentLaserScript = currentLaser.GetComponent<LaserProj>();
            currentLaserScript.speed = 1000f;
            currentLaserScript.strength = damage;
        }
    }
    internal override void Move()
    {
        transform.LookAt(Player.transform.position);

        if (Vector3.Distance(transform.position, Player.transform.position) <= MinDist)
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }


    }
}