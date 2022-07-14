using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaxHpItem : Items
{

    // Start is called before the first frame update
    void Start()
    {
        itemName = "Alien fruit";
        itemDescription = "You're more healthy! (max hp up)";
        maxHpBonus = 25;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
