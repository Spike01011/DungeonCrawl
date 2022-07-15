using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LaserProj : MonoBehaviour
{
    public ParticleSystem hitParticles;
    internal float strength;
    internal float speed;
    private Rigidbody rb;
    private float endTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        endTime = Time.time + 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.deltaTime;
        if (endTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Instantiate(hitParticles, collider.transform.position, hitParticles.transform.rotation);
            collider.gameObject.GetComponent<PlayerController>().takeDamage(strength);
        }
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
