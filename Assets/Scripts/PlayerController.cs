using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    private GameObject cameraFocusPoint;
    private Rigidbody rb;

    [SerializeField] private float speed = 10000f;
    public float speedMult = 1.0f;
    [SerializeField] private float runAngleModifier = 0;
    [SerializeField] private float jumpDurationMulti = 1.0f;
    [SerializeField] private float actualSpeed;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float oldspeedMult = 1.0f;
    public float hp = 100;
    [SerializeField]private float attackSpeed = 1;
    public float attackSpeedMult = 1.0f;
    public float hpMult = 1.0f;
    [SerializeField] int actualHp;


    private int runForward = 0;
    private int runLeft = -90;
    private int runBackward = 180;
    private int runRight = 90;
    private int runForwardLeft = -45;
    private int runForwardRight = 45;
    private int runBackwardLeft = -135;
    private int runBackwardRight = 135;

    private bool isGrounded = true;
    private bool w;
    private bool a;
    private bool s;
    private bool d;
    private bool isRunning = false;

    private int jumpHash;
    private int speedMultHash;
    private int jumpDurationMultHash;
    private int isRunningHash;


    // Start is called before the first frame update
    void Start()
    {
        cameraFocusPoint = GameObject.Find("Focus");
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        speedMultHash = Animator.StringToHash("SpeedMult");
        jumpHash = Animator.StringToHash("Jump");
        isRunningHash = Animator.StringToHash("IsRunning");
        playerAnim.SetFloat(speedMultHash, speedMult);
        jumpDurationMultHash = Animator.StringToHash("JumpMotionMult");
        playerAnim.SetFloat(jumpDurationMultHash, jumpDurationMulti);

        speed = 500f;
        speedMult = 1.0f;
        jumpDurationMulti = 1.0f;
        jumpForce = 500f;
        oldspeedMult = 10000f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualHp = Convert.ToInt32(hp * this.hpMult);
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerAnim.SetTrigger(jumpHash);
            rb.velocity = transform.up * Time.deltaTime * jumpForce;
        }

        actualSpeed = speed * speedMult;
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

        //transform.rotation = Quaternion.Euler(0, runAngleModifier, 0);
        //transform.rotation = cameraFocusPoint.transform.rotation;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraFocusPoint.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.Rotate(Vector3.up, runAngleModifier);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraFocusPoint.transform.rotation.eulerAngles.y + runAngleModifier, transform.rotation.eulerAngles.z);

        if (speedMult != oldspeedMult)
        {
            oldspeedMult = speedMult;
            playerAnim.SetFloat(speedMultHash, speedMult);
        }

        if (isRunning != playerAnim.GetBool(isRunningHash))
        {
            playerAnim.SetBool(isRunningHash, isRunning);
        }

        if (isRunning)
        {
            rb.velocity = transform.forward * Time.deltaTime * actualSpeed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        //rb.velocity = Input.GetAxis("Horizontal") == 0 ? cameraFocusPoint.transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * actualSpeed : Input.GetAxis("Vertical") == 0 ? cameraFocusPoint.transform.forward * Input.GetAxis("Horizontal") * Time.deltaTime * actualSpeed : cameraFocusPoint.transform.forward * (Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"))/2 * Time.deltaTime * actualSpeed;
    }
}
