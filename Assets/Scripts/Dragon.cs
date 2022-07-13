using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Dragon : MyEntity
{
    private Animator entityAnim;

    public float health = 50f;
    public float damage = 2f;
    public float attackSpeed = 1f;

    public Transform Player;
    int MoveSpeed = 2;
    int MaxDist = 15;
    float MinDist = 1.8f ;

    private bool isDie = false;
    private bool isWalk = false;

    private int isDieHash;
    private int isWalkHash;
    private int isAttackHash;



    internal override void Start()
    {
        entityAnim = GetComponent<Animator>();
        isDieHash = Animator.StringToHash("isDie");
        isWalkHash = Animator.StringToHash("isWalk");
        isAttackHash = Animator.StringToHash("Attack");
    }
    void FixedUpdate()
    {
        transform.LookAt(Player);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (Vector3.Distance(transform.position, Player.position) <= MinDist)
        {
            isWalk = false;
            entityAnim.SetBool(isWalkHash, isWalk);
            entityAnim.SetTrigger(isAttackHash);
            
        }
        else
        {
            isWalk = true;
            entityAnim.SetBool(isWalkHash, isWalk);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }

    IEnumerator FuckYou()
    {
        yield return new WaitForSeconds(1);
        entityAnim.SetTrigger(isAttackHash);
    }
    
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        isWalk = false;
        isDie = true;
        entityAnim.SetBool(isDieHash, isDie);
        Destroy(transform.gameObject);

    }

}
