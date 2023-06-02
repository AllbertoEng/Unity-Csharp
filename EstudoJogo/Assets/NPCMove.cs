using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCMove : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Transform moveTo;
    [SerializeField] float speed = 3f;
    [SerializeField] float distance = 1f;

    Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        moveTo = GameManager.instance.player.transform;
    }

    private void FixedUpdate()
    {
        if (moveTo == null)
            return;

        Vector3 direction = ( moveTo.position - transform.position).normalized;

        if (Vector3.Distance(moveTo.position, transform.position) <= distance)
        {
            animator.SetBool("moving", false);
            rb2d.velocity = Vector3.zero;
            return;
        }

        //walking direction animation            
        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);

        //stopped direction animation
        animator.SetFloat("lastHorizontal", direction.x);
        animator.SetFloat("lastVertical", direction.y);

        animator.SetBool("moving", true);
        rb2d.velocity = direction * speed;
    }

    private void StopMoving()
    {
        moveTo = null;
        rb2d.velocity = Vector3.zero;
    }
}
