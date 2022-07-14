using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDrone : MyEntity
{
    public GameObject laserPrefab;
    public GameObject specialPrefab;
    public Camera cam;
    private GameObject Player;
    private PlayerController playerController;

    private float range = 200f;
    internal float damage;
    internal float attackSpeed;
    internal float currentShotDamage;
    float MinDist = 10f;
    public Vector3 route;
    int counter = 0;




    // Start is called before the first frame update
    internal override void Start()
    {
        Player = GameObject.Find("Player");

        playerController = Player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();


        baseDamage = 10f;
        baseAttackSpeed = 1f;
        baseHp = 200f;
        baseSpeed = 200f;

        speedMult = 1.0f;
        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        hpMult = 1.0f;

        InvokeRepeating("Shoot", 1, 1);

    }

    // Update is called once per frame
    internal override void FixedUpdate()
    {

        hp = baseHp * hpMult;
        speed = baseSpeed * speedMult;
        damage = baseDamage * damageMult;
        attackSpeed = baseAttackSpeed * attackSpeedMult;
        Move();


        //route = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        //var currentLaser = Instantiate(laserPrefab, this.transform.position + Vector3.forward * 2, Quaternion.identity, this.transform);
        //currentLaser.transform.LookAt(route);
        //currentLaser.transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //var currentLaserScript = currentLaser.GetComponent<LaserProj>();
        //currentLaserScript.speed = 100f;
        //currentLaserScript.strength = damage;



    }

    void Shoot()
    {

        var currentLaser = Instantiate(laserPrefab, transform.position, transform.rotation);
        currentLaser.transform.LookAt(Player.transform.position);
        currentLaser.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        var currentLaserScript = currentLaser.GetComponent<LaserProj>();
        currentLaserScript.speed = 1000f;
        currentLaserScript.strength = damage;
        


        //Instantiate(laser, Player.transform);
        //RaycastHit hit;
        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        //
            //if (hit.transform.CompareTag("Player"))
            //{

           // }
        //};
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