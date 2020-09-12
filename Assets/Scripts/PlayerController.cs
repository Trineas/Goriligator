using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody rb;
    public float moveSpeed, jumpForce;

    private Vector2 moveInput;

    public Animator anim, flipAnim;
    public SpriteRenderer sr;

    private bool movingBackwards;

    public bool stopMove;

    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    public Transform camTarget;
    public float aheadAmount, aheadSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    void Update()
    {
        if (!stopMove)
        {
            // Movement
            moveInput.x = Input.GetAxisRaw("Horizontal");
            //moveInput.y = Input.GetAxisRaw("Vertical");
            //moveInput.Normalize();

            rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);

            anim.SetFloat("moveSpeed", rb.velocity.magnitude);

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
                rb.velocity = new Vector3(0f, jumpForce, 0f);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * .5f);
            }

            anim.SetBool("onGround", isGrounded);
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

        anim.SetBool("movingBackwards", movingBackwards);

        // Cam Target Look Ahead
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }
}
