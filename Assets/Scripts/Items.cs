using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MyMasterEntity
{

    public Items()
    {
        movementSpeedMulti = 0;
        attackSpeedMulti = 0;
        maxHpBonus = 0;
        percentageMaxHpBonus = 0;
    }
    
    // Start is called before the first frame update
    internal override void Start()
    { 

    }

    // Update is called once per frame
    internal override void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            player.hp += maxHpBonus;
            player.hpMult += percentageMaxHpBonus;
            player.attackSpeedMult += attackSpeedMulti;
            player.speedMult += movementSpeedMulti;
            Destroy(gameObject);
        }
    }

}
