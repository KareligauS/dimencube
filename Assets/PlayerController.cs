using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool canMove = true; 
    private float moveInput;
    private float groundDist = 3.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!canMove) return;
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist);

    }
}