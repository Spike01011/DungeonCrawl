using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyEntity : MyMasterEntity
{
    private Animator anim;
    private Rigidbody rb;

    public float baseHp = 100f;
    public float baseDamage = 10f;
    public float baseAttackSpeed = 1f;
    public float baseSpeed = 10000f;

    public float speed;
    public float damage;
    public float hp;
    public float attackSpeed;

    public bool isRunning = false;

    public int isRunningHash;

    public MyEntity()
    {
        maxHpBonus = 0;
        movementSpeedMulti = 1.0f;
        attackSpeedMulti = 1.0f;
        percentageMaxHpBonus = 1.0f;
        damageMult = 1.0f;
    }
    
    // Start is called before the first frame update


    // Update is called once per frame


    internal abstract void Move();
}
