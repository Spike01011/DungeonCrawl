using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerController : MyEntity
{
    public ParticleSystem blinkParticles;
    private GameObject cameraFocusPoint;
    private UiManager uiManager;

    public bool isDead;
    public int kills;


    [SerializeField] private float runAngleModifier = 0;
    [SerializeField] private float jumpDurationMulti;
    [SerializeField] private float jumpForce;
    [SerializeField] private float oldspeedMult;
    internal float critChance = 0;
    internal float level;
    internal float xpForLevelUp;
    internal float blinkDistance;


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
    private float blinkTimestamp;
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
        Cursor.lockState = CursorLockMode.Locked;
        kills = 0;
        isDead = false;
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
        Player = gameObject;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        cameraFocusPoint = GameObject.Find("Focus");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        speedMultHash = Animator.StringToHash("SpeedMult");
        jumpHash = Animator.StringToHash("Jump");
        isRunningHash = Animator.StringToHash("IsRunning");
        anim.SetFloat(speedMultHash, speedMult);
        jumpDurationMultHash = Animator.StringToHash("JumpMotionMult");
        anim.SetFloat(jumpDurationMultHash, jumpDurationMulti);
        blinkTimestamp = Time.time + 1;

        level = 1;
        xpForLevelUp = 100;

        baseDamage = 20f;
        baseAttackSpeed = 1.5f;
        baseSpeed = 500f;
        baseHp = 100;
        jumpForce = 15f;

        attackSpeedMult = 1.0f;
        damageMult = 1.0f;
        this.speedMult = 1.0f;
        jumpDurationMulti = 0.2f;
        oldspeedMult = 1.0f;
        hpMult = 1.0f;

        anim.SetFloat(speedMultHash, speedMult);
        anim.SetFloat(jumpDurationMultHash, jumpDurationMulti);

        maxHp = baseHp * hpMult;
        hp = maxHp;
        InvokeRepeating("HpRegen", 1, 1);
    }

    // Update is called once per frame
    internal override void FixedUpdate()
    {
        LevelUpCheck();
        damage = baseDamage * damageMult;
        speed = baseSpeed * speedMult;
        blinkDistance = speed / 30;
        attackSpeed = baseAttackSpeed * attackSpeedMult;
        Blink();
        Jump();

        maxHp = Convert.ToInt32(baseHp * hpMult);
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);

        speed = baseSpeed * speedMult;
        Move();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DragonJaw"))
        {
        }
    }

    internal void Blink()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (blinkTimestamp <= Time.time)
            {
                Instantiate(blinkParticles, transform.position, blinkParticles.transform.rotation);
                Vector3 destination = transform.position + transform.forward * blinkDistance;
                transform.position = destination;
                Instantiate(blinkParticles, transform.position, blinkParticles.transform.rotation);
                uiManager.BlinkCooldown = 4;
                blinkTimestamp = Time.time + 4; 
            }
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
        int i = 0;
        if (isRunning)
        {
            i = 1;
            //rb.velocity = transform.forward * Time.deltaTime * speed;
        }
        else
        {   i=0;
            //rb.velocity = new Vector3(0, 0, 0);

        }
        Vector3 velocity = ((transform.forward * i)) * speed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger(jumpHash);
            //rb.velocity = transform.up * Time.deltaTime * jumpForce;
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void LevelUpCheck()
    {
        if (experience >= xpForLevelUp)
        {
            LevelUp();
        }

    }

    void LevelUp()
    {
        level += 1;
        experience -= xpForLevelUp;
        xpForLevelUp *= 1.5f;

        baseHp *= 1.1f;
        baseSpeed *= 1.1f;
        baseDamage *= 1.1f;
        baseAttackSpeed *= 1.1f;
    }

    void HpRegen()
    {
        if (hp < maxHp)
        {
            hp += Convert.ToInt32(MathF.Ceiling(hp/100));
        }

        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Application.Quit(0);
    }

}