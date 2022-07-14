using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyEntity : MyMasterEntity
{
    internal GameObject Player;
    internal SpawnManager spawnManager;
    internal Animator anim;
    internal Rigidbody rb;

    public float baseHp = 100f;
    public float baseDamage = 10f;
    public float baseAttackSpeed = 1f;
    public float baseSpeed = 10000f;
    public float experience;
    public float experienceMult = 1.0f;

    public float speed;
    public float damage;
    public float hp;
    public float attackSpeed;

    public bool isRunning = false;

    public int isRunningHash;


    // Start is called before the first frame update


    // Update is called once per frame


    internal abstract void Move();

    public void takeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        }
        Debug.Log(hp);
    }

    public void Die()
    {
        Player.GetComponent<PlayerController>().experience += experience * experienceMult;
        spawnManager.experience += experience * experienceMult;
        Destroy(transform.gameObject);
    }
}
