using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControler2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    public Vector2 lastMotionVector;
    Animator animator;
    public bool moving;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"),
            vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(horizontal, vertical);

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if (moving)
        {
            //walking direction animation            
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);

            //stopped direction animation
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * speed;
    }
}
