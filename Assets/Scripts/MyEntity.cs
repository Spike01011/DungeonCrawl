using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyEntity : MyMasterEntity
{
    internal GameObject Player;
    internal SpawnManager spawnManager;
    internal Animator anim;
    internal Rigidbody rb;

    internal float baseHp = 100f;
    internal float baseDamage = 10f;
    internal float baseAttackSpeed = 1f;
    internal float baseSpeed = 10000f;
    internal float experience;
    internal float experienceMult = 1.0f;

    internal float speed;
    internal float damage;
    internal float hp;
    internal float attackSpeed;

    internal bool isRunning = false;

    internal int isRunningHash;


    // Start is called before the first frame update


    // Update is called once per frame


    internal abstract void Move();

    public void takeDamage(float amount, string target="player")
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
        try
        {

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Die()
    {
        Player.GetComponent<PlayerController>().experience += experience * experienceMult;
        spawnManager.experience += experience * experienceMult;
        spawnManager.TryToSpawnItem(new Vector3(transform.position.x, 0.5f, transform.position.z));
        Destroy(gameObject);
    }
}
