using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyMasterEntity : MonoBehaviour
{
    internal float speedMult;
    internal float attackSpeedMult;
    internal float damageMult;
    internal float hpMult;

    // Start is called before the first frame update
    internal abstract void Start();

    // Update is called once per frame
    internal abstract void FixedUpdate();
}
