using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : MonoBehaviour
{
    internal float strength;
    internal float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerController>().takeDamage;
        }
        else if (!collider.gameObject.CompareTag("Drone"))
        {
            Destroy(this.gameObject);
        }
    }
}
