using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Movement
    /// </summary>
    [SerializeField] private float speed;
    private Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D boxCollider2D;

    /// <summary>
    /// Ground Check
    /// </summary>
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGround = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {

        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(xMovement * transform.right);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())          // Space e basıldığında ve yerde ise
        {
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    private bool isGrounded()
    {

        float extraHeight = .1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeight, groundLayer);
        Color rayClolor;
        if (hitInfo.collider != null)
        {
            rayClolor = Color.green;
        }
        else
        {
            rayClolor = Color.red;
        }
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeight), rayClolor);
        print(hitInfo.collider);
        return hitInfo.collider != null;
    }
}
