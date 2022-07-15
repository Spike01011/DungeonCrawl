using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroneController : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject specialPrefab;
    public Camera cam;
    private GameObject player;
    private PlayerController playerController;

    //LineRenderer laserLine;

    private float range = 200f;
    internal float damage;
    internal float critChance;
    internal float attackSpeed;
    internal float currentShotDamage;
    private float timestamp;

    GameObject focus;
    // Start is called before the first frame update
    void Start()
    {
        //laserLine = GetComponent<LineRenderer>();

        timestamp = Time.time + 1;
        player = GameObject.Find("Player");
        focus = GameObject.Find("Focus");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        damage = playerController.damage;
        attackSpeed = playerController.attackSpeed;
        critChance = playerController.critChance;


        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (timestamp <= Time.time)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                transform.rotation = Quaternion.Euler(focus.transform.rotation.eulerAngles.x, focus.transform.rotation.eulerAngles.y - 5, 0);

                try
                {
                    ToCritOrNotToCrit();
                    var playerLaser = Instantiate(laserPrefab, transform.position, focus.transform.rotation);
                    playerLaser.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    var laserScript = playerLaser.GetComponent<LaserPlayer>();
                    laserScript.speed = 6000f;
                    laserScript.strength = damage;

                    hit.transform.gameObject.GetComponent<MyEntity>().takeDamage(damage, "enemy");
                }
                catch (Exception e)
                {
                }
                timestamp = Time.time + 1 / attackSpeed;
            };
        }
    }

    void ToCritOrNotToCrit()
    {
        if (critChance > 0 && critChance < 100)
        {
            var chanceOutcome = Random.Range(1, 10);
            if (chanceOutcome <= critChance)
            {
                currentShotDamage = damage * 2;
            }
        }
        else if (critChance == 0)
        {
            currentShotDamage = damage;
        }
        else if (critChance == 100)
        {
            currentShotDamage = damage * 2;
        }
    }
}
