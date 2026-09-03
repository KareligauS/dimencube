using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool canMove = true; 
    private float moveInput;
    private float groundDist = 3.1f;

    public float groundCheck = 2f;
    public float slide = 2f;
    public bool isOnIce = false;
    public float fallMult = 3f;

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

        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, groundCheck, Vector3.down, out hit, groundCheck);

        isOnIce = isGrounded && hit.collider != null && hit.collider.CompareTag("Slippery");
        float targetSpeed = moveInput * moveSpeed;
        if (isOnIce)
        {
            float iceSpeed = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, Time.fixedDeltaTime * slide);
            rb.linearVelocity = new Vector3(iceSpeed, rb.linearVelocity.y, 0);
        }
        else
        {
        rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
        }

        if (rb.linearVelocity.y < 0)
        {
          rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.fixedDeltaTime;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist);

    }
}