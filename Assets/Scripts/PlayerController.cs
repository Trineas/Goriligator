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

    public LayerMask whatIsGround, whatIsGrabbable;
    public Transform groundPoint;
    private bool isGrounded;

    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    public bool doubleJumpEnabled, wallJumpEnabled;

    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public float wallJumpTime = .1f;
    private float wallJumpCounter;
    private Vector3 originalPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    void Start()
    {
        originalPosition = wallGrabPoint.transform.localPosition;
    }

    void Update()
    {
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
            else
            {
                isGrounded = false;
            }

            // Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
                }
            }

            if (isGrounded)
            {
                jumpCounter = 0;
            }

            // Wall Jump
            if (wallJumpEnabled)
            {
                RaycastHit stick;
                if (Physics.Raycast(wallGrabPoint.position, Vector3.right, out stick, .2f, whatIsGrabbable) || Physics.Raycast(wallGrabPoint.position, Vector3.left, out stick, .2f, whatIsGrabbable))
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
            wallGrabPoint.transform.localPosition = new Vector3(-.75f, originalPosition.y, originalPosition.z);
        }
        else
        {
            wallGrabPoint.transform.localPosition = originalPosition;
        }

        anim.SetBool("movingBackwards", movingBackwards);
        anim.SetBool("onWall", isGrabbing);
        anim.SetBool("onGround", isGrounded);
        anim.SetFloat("moveSpeed", rb.velocity.magnitude);

        // Cam Target Look Ahead
        /*if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }*/
    }
}
