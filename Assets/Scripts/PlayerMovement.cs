using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Movement
    /// </summary>
    [SerializeField] private float speed;
    //private Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D boxCollider2D;

    /// <summary>
    /// Ground Check
    /// </summary>
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGround = false;

    /// <summary>
    /// My gravity settings
    /// </summary>
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityDivide = 10f;
    [SerializeField] private float jumpHeight = 20f;
    private Vector3 velocity;

    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();                      // Rigidbody fizikleri çok da kullanışlı değil.
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Update to FPS
    /// </summary>
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(xMovement * transform.right);

        // Space e basılı ve Yerdeyse kuvvet uygula
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded())          // Space e basıldığında ve yerde ise
        //{
        //    rigidbody.velocity = Vector2.up * jumpForce;
        //}


        // İf player is not on the ground apply force -gravity with Time
        if (!isGrounded())
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;
        }
        //İf player is on the ground apply so small force
        else
        {
            velocity.y = -0.000005f;
        }                    

        // when prees Space and isGround() use force jumpHeight and gravity
        // this formule usually using jump operations. Mathf.Sqrt(2*jumpHeight*gravity)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * Time.deltaTime / gravityDivide) ;
        }

        transform.Translate(velocity);
    }

    /// <summary>
    /// isGround==Green / isGround!=Red
    /// </summary>
    /// <returns></returns>
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
