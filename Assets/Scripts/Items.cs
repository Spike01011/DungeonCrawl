using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected float movementSpeedMulti;
    private float AttackSpeedMulti;
    private float MaxHpBonus;
    private float PercentageMaxHpBonus;
    
    public Items(float movementSpeedMulti, float attackSpeedMulti, float maxHpBonus, float percentageMaxHpBonus)
    {
        this.movementSpeedMulti = movementSpeedMulti;
        AttackSpeedMulti = attackSpeedMulti;
        MaxHpBonus = maxHpBonus;
        PercentageMaxHpBonus = percentageMaxHpBonus;
    }
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
            player.hp += MaxHpBonus;
            player.hpMult += PercentageMaxHpBonus;
            player.attackSpeedMult += AttackSpeedMulti;
            player.speedMult += movementSpeedMulti;
            Destroy(gameObject);
        }
    }

}
