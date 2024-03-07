using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 45f;
    [SerializeField] private bool airControl = false;
    [Range(0, .1f)] [SerializeField] private float movementSmooth = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    const float groundedRadius = .2f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Vector3 velocity = Vector3.zero;
    private int pickupCounter = 0;

    [SerializeField] private Image life1;
    [SerializeField] private Image life2;
    [SerializeField] private Image life3;
    [SerializeField] private Image noLife1;
    [SerializeField] private Image noLife2;
    [SerializeField] private Image noLife3;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmooth);
            if (move > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (move < 0 && isFacingRight)
            {
                Flip();
            }
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            airControl = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            pickupCounter++;
            Debug.Log(pickupCounter.ToString());
        }
    }

    private void HUDController()
    {
        noLife1.gameObject.SetActive(false);
        noLife2.gameObject.SetActive(false);
        noLife3.gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.F)) //this part will need to go into an update function after setting up collisions with rats
        {
            noLife3.gameObject.SetActive(true);
            life3.gameObject.SetActive(false);
            Debug.Log("F button pressed.");
        }
    }
}
