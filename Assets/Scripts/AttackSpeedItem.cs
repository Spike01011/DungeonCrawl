using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackSpeedItem : Items
{
    // Start is called before the first frame update
    void Start()
    {
        itemName = "spray 'n' pray";
        itemDescription = "Attack Speed up!";
        attackSpeedMult = 0.1f;
    }

    // Update is called once per frame
}
