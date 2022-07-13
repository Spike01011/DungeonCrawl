using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected float movementSpeedMulti;
    protected float attackSpeedMulti;
    protected float maxHpBonus;
    protected float percentageMaxHpBonus;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
