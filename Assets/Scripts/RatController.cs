using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{

    public Animator animator;
    private float timePassed = 0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        timePassed += Time.deltaTime;
        rb.isKinematic = true;
        if (timePassed > 5f)
        {
            Destroy(gameObject);
        }
    }
}
