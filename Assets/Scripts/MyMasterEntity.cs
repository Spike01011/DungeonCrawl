using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyMasterEntity : MonoBehaviour
{
    protected float movementSpeedMulti;
    protected float attackSpeedMulti;
    protected float maxHpBonus;
    protected float percentageMaxHpBonus;
    // Start is called before the first frame update
    internal abstract void Start();

    // Update is called once per frame
    internal abstract void Update();
}
