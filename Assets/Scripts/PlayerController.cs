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

    [SerializeField] private Image[] lives = new Image[3];
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite brokenHeart;

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

    private void Update()
    {
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

        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {   
        Sprite lives2 = lives[2].gameObject.GetComponent<Image>().sprite;
        Sprite lives1 = lives[1].gameObject.GetComponent<Image>().sprite;
        Sprite lives0 = lives[0].gameObject.GetComponent<Image>().sprite;

        if (lives2 == fullHeart)
        {
            lives[2].gameObject.GetComponent<Image>().sprite = brokenHeart;
        }
        if (lives2 == brokenHeart && lives1 == fullHeart)
        {
            lives[1].gameObject.GetComponent<Image>().sprite = brokenHeart;
        }
        if (lives1 == brokenHeart && lives0 == fullHeart)
        {
            lives[0].gameObject.GetComponent<Image>().sprite = brokenHeart;
        }
    }
}
