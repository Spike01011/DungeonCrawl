using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PercentageMaxHpItem : Items
{

    // Start is called before the first frame update
    void Start()
    {
        itemName = "Delicous Donut";
        itemDescription = "more mass, more endurance (max hp % bonus)";
        hpMult = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
