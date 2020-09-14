using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody rb;
    public float moveSpeed, jumpForce, doubleJumpForce;
    private int jumpCounter;

    private Vector2 moveInput;

    public Animator anim, flipAnim;
    public SpriteRenderer sr;

    private bool movingBackwards;

    public bool stopMove;

    public LayerMask whatIsGround, whatIsGrabbable, whatIsBoth;
    public Transform groundPoint;
    private bool isGrounded;

    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    public bool doubleJumpEnabled, wallJumpEnabled, jumpEnabled, runEnabled;

    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public float wallJumpTime = .1f;
    private float wallJumpCounter;
    private Vector3 originalPosition;

    public ParticleSystem footstepsEffect, impactEffect;
    public ParticleSystem.EmissionModule footEmission;

    public int jumpSound, doubleJumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    void Start()
    {
        originalPosition = wallGrabPoint.transform.localPosition;
        footEmission = footstepsEffect.emission;
    }

    void Update()
    {
        if (stopMove)
        {
            rb.velocity = Vector3.zero;
        }

        if (!stopMove && wallJumpCounter <= 0)
        {
            // Movement
            moveInput.x = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);

            // Groundcheck
            RaycastHit hit;
            if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsGround))
            {
                isGrounded = true;
            }
            else if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsBoth))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (jumpEnabled)
            {
                // Jump
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(jumpSound);
                }

                if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
                }

                // Double Jump
                if (doubleJumpEnabled)
                {
                    if (Input.GetButtonDown("Jump") && jumpCounter == 0 && !isGrounded)
                    {
                        jumpCounter++;
                        rb.velocity = new Vector2(0f, doubleJumpForce);
                        AudioManager.instance.PlaySFX(doubleJumpSound);

                        anim.SetTrigger("DoubleJump");

                        impactEffect.gameObject.SetActive(true);
                        impactEffect.Stop();
                        impactEffect.transform.position = footstepsEffect.transform.position;
                        impactEffect.Play();
                    }
                }

                if (isGrounded)
                {
                    jumpCounter = 0;
                }
            }

            // Wall Jump
            if (wallJumpEnabled)
            {
                RaycastHit stick;
                if (Physics.Raycast(wallGrabPoint.position, Vector3.right, out stick, .2f, whatIsGrabbable) || Physics.Raycast(wallGrabPoint.position, Vector3.left, out stick, .2f, whatIsGrabbable))
                {
                    canGrab = true;
                }
                else if (Physics.Raycast(wallGrabPoint.position, Vector3.right, out stick, .2f, whatIsBoth) || Physics.Raycast(wallGrabPoint.position, Vector3.left, out stick, .2f, whatIsBoth))
                {
                    canGrab = true;
                }
                else
                {
                    canGrab = false;
                }

                isGrabbing = false;

                if (canGrab && !isGrounded)
                {
                    if ((moveInput.x > 0f) || (moveInput.x < 0f))
                    {
                        isGrabbing = true;
                    }
                }

                if (isGrabbing)
                {
                    rb.useGravity = false;
                    rb.velocity = Vector2.zero;

                    if (Input.GetButtonDown("Jump"))
                    {
                        wallJumpCounter = wallJumpTime;

                        rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * moveSpeed, jumpForce);
                        rb.useGravity = true;
                        isGrabbing = false;
                    }
                }
                else
                {
                    rb.useGravity = true;
                }
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        // Sprite Flip
        if (!sr.flipX && moveInput.x < 0f)
        {
            sr.flipX = true;
            //flipAnim.SetTrigger("Flip");
        }
        else if (sr.flipX && moveInput.x > 0f)
        {
            sr.flipX = false;
            //flipAnim.SetTrigger("Flip");
        }

        if (!movingBackwards && moveInput.y > 0f)
        {
            movingBackwards = true;
            //flipAnim.SetTrigger("Flip");
        }
        else if (movingBackwards && moveInput.y < 0f)
        {
            movingBackwards = false;
            //flipAnim.SetTrigger("Flip");
        }

        if (sr.flipX)
        {
            wallGrabPoint.transform.localPosition = new Vector3(-1f, originalPosition.y, originalPosition.z);
        }
        else
        {
            wallGrabPoint.transform.localPosition = originalPosition;
        }

        anim.SetBool("movingBackwards", movingBackwards);
        anim.SetBool("onWall", isGrabbing);
        anim.SetBool("onGround", isGrounded);
        anim.SetFloat("moveSpeed", rb.velocity.magnitude);

        // Footstep & Impact Effect
        if (moveInput.x > 0 && runEnabled || moveInput.x < 0 && runEnabled)
        {
            footEmission.rateOverTime = 35f;
        }
        else
        {
            footEmission.rateOverTime = 0f;
        }

        if (!isGrounded)
        {
            footEmission.rateOverTime = 0f;
        }
    }
}
