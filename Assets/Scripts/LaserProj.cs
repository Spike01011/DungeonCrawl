using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : MonoBehaviour
{
    private float stength;
    private float speed;
    private Rigidbody rb;
    public LaserProj(float strength, float speed)
    {
        this.stength = strength;
        this.speed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.Rotate(Vector3.forward, 90);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
    }
}
