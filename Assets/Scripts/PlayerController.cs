using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerController : MyEntity
{
    private GameObject cameraFocusPoint;


    [SerializeField] private float runAngleModifier = 0;
    [SerializeField] private float jumpDurationMulti;
    [SerializeField] private float jumpForce;
    [SerializeField] private float oldspeedMult;
    internal float critChance = 0;


    private int runForward = 0;
    private int runLeft = -90;
    private int runBackward = 180;
    private int runRight = 90;
    private int runForwardLeft = -45;
    private int runForwardRight = 45;
    private int runBackwardLeft = -135;
    private int runBackwardRight = 135;
    internal float maxHp;

    private bool isGrounded = true;
    private bool w;
    private bool a;
    private bool s;
    private bool d;

    private int jumpHash;
    private int speedMultHash;
    private int jumpDurationMultHash;




    // Start is called before the first frame update
    internal override void Start()
    {
        cameraFocusPoint = GameObject.Find("Focus");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        speedMultHash = Animator.StringToHash("SpeedMult");
        jumpHash = Animator.StringToHash("Jump");
        isRunningHash = Animator.StringToHash("IsRunning");
        anim.SetFloat(speedMultHash, speedMult);
        jumpDurationMultHash = Animator.StringToHash("JumpMotionMult");
        anim.SetFloat(jumpDurationMultHash, jumpDurationMulti);

        baseDamage = 20f;
        baseAttackSpeed = 1.5f;
        baseSpeed = 500f;
        baseHp = 100;
        jumpForce = 500f;

        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        this.speedMult = 1.0f;
        jumpDurationMulti = 1.0f;
        oldspeedMult = 1.0f;
        hpMult = 1.0f;

        anim.SetFloat(speedMultHash, speedMult);

        maxHp = baseHp * hpMult;
        hp = maxHp;
    }

    // Update is called once per frame
    internal override void FixedUpdate()
    {
        damage = baseDamage * damageMult;
        speed = baseSpeed * speedMult;
        attackSpeed = baseAttackSpeed * attackSpeedMult;

        hp = Convert.ToInt32(baseHp * hpMult);
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);

        Jump();

        speed = baseSpeed * speedMult;
        Move();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DragonJaw"))
        {
        }
    }

    internal override void Move()
    {
        if (w || a || s || d)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (w && a)
        {
            runAngleModifier = runForwardLeft;
        }
        else if (w && d)
        {
            runAngleModifier = runForwardRight;
        }
        else if (s && d)
        {
            runAngleModifier = runBackwardRight;
        }
        else if (s && a)
        {
            runAngleModifier = runBackwardLeft;
        }
        else if (w)
        {
            runAngleModifier = runForward;
        }
        else if (a)
        {
            runAngleModifier = runLeft;
        }
        else if (s)
        {
            runAngleModifier = runBackward;
        }
        else if (d)
        {
            runAngleModifier = runRight;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraFocusPoint.transform.rotation.eulerAngles.y + runAngleModifier, transform.rotation.eulerAngles.z);

        if (speedMult != oldspeedMult)
        {
            oldspeedMult = speedMult;
            anim.SetFloat(speedMultHash, speedMult);
        }

        if (isRunning != anim.GetBool(isRunningHash))
        {
            anim.SetBool(isRunningHash, isRunning);
        }

        if (isRunning)
        {
            rb.velocity = transform.forward * Time.deltaTime * speed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger(jumpHash);
            rb.velocity = transform.up * Time.deltaTime * jumpForce;
        }
    }
}