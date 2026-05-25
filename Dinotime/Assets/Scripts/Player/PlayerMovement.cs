using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    public float speed = 8f;

    [Header("Jumping")]
    public float jumpingPower = 16f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTime = 0.2f;

    private bool isJumping;
    private float horizontal;
    private bool isFacingRight = true;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool canDoubleJump = false;

    [Header("Dashing")]
    public float dashPower;
    public float dashTime;
    public float dashCooldownTime;
    private bool canDash = true;
    private bool isDashing = false;

    internal bool dashAbilityActivated = false;
    internal bool doubleJumpAbilityActivated = false;

    private bool canMove = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public void StartGame()
    {
        canMove = true;
    }

    public void StopCamera()
    {
        transform.Find("FollowCamera").gameObject.SetActive(false);

        rb.linearVelocity = Vector2.zero;
    }

    public void StartCamera()
    {
        transform.Find("FollowCamera").gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!canMove)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;

            canDoubleJump = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpBufferCounter = 0f;

            canDoubleJump = true;

            //StartCoroutine(JumpCooldown());
        }
        else if (doubleJumpAbilityActivated && canDoubleJump && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpBufferCounter = 0f;

            canDoubleJump = false;
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();
        DashInput();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void DashInput()
    {
        if (!dashAbilityActivated)
            return;

        if (!canDash)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("Dash");
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float origGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = origGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldownTime);

        canDash = true;
    }
}