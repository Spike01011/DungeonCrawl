using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : Items
{
    // Start is called before the first frame update
    void Start()
    {
        itemName = "Rusty sword";
        itemDescription = "Gives bonus Tetanus Damage. (damage up)";
        damageMult = 0.1f;
    }

    // Update is called once per frame
}
