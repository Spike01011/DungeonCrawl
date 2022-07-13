using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyEntity : MyMasterEntity
{
    public float hp = 100f;
    public float damage = 10f;
    public float movementSpeed = 200f;
    public float attackSpeed = 1f;
    public float damageMult = 1.0f;

    public MyEntity()
    {
        maxHpBonus = 0;
        movementSpeedMulti = 1.0f;
        attackSpeedMulti = 1.0f;
        percentageMaxHpBonus = 1.0f;
    }
    
    // Start is called before the first frame update


    // Update is called once per frame


    internal abstract void Move();
}
