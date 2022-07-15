using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MyMasterEntity
{
    public GameObject infoText;

    internal float critChance = 0;
    internal float maxHpBonus = 0;
    internal string itemName;
    internal string itemDescription;

    // Start is called before the first frame update
    internal override void Start()
    {
        speedMult = 0;
        attackSpeedMult = 0;
        damageMult = 0;
        hpMult = 0;
    }

    // Update is called once per frame
    internal override void FixedUpdate()
    {
        transform.Rotate(Vector3.up);
    }

    void OnCollisionEnter(Collision other)
    {
        //ItemIndicator indicator = Instantiate(infoText, transform.position, Quaternion.identity).GetComponent<ItemIndicator>();
        //indicator.SetDamageText($"+{hpMult} hp");
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            player.baseHp += maxHpBonus;
            player.hpMult += hpMult;
            player.attackSpeedMult += attackSpeedMult;
            player.speedMult += speedMult;
            player.critChance += critChance;
            player.damageMult += damageMult;
            Destroy(gameObject);
        }
    }



}
