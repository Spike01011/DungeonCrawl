using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject specialPrefab;
    public Camera cam;
    private GameObject player;
    private PlayerController playerController;

    private float range = 200f;
    internal float damage;
    internal float critChance;
    internal float attackSpeed;
    internal float currentShotDamage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        damage = playerController.damage;
        attackSpeed = playerController.attackSpeed;
        critChance = playerController.critChance;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {

            }
        };
    }

    void ToCritOrNotToCrit()
    {
        if (critChance > 0 && critChance < 100)
        {

        }
    }
}
