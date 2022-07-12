using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject laserPrefab;

    public GameObject specialPrefab;

    public Camera cam;

    private float range = 200f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
       if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range));
    }
}
