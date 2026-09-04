using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool wasGrounded;
    public bool canMove = true;
    private float moveInput;
    private float groundDist = 3.1f;

    public float groundCheck = 3f;
    public float slide = 2f;
    public bool isOnIce = false;
    public float fallMult = 3f;

    [Inject] private readonly AudioManager _audioManager;

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
            _audioManager.PlaySFX(AudioClipEnum.Jump);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            _audioManager.StopContinuousSFX(AudioClipEnum.Walking);
            _audioManager.StopContinuousSFX(AudioClipEnum.Sliding);
            return;
        }

        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, groundCheck, Vector3.down, out hit, groundCheck);

        if (isGrounded && !wasGrounded)
        {
            _audioManager.PlaySFX(AudioClipEnum.Fall);
        }
        wasGrounded = isGrounded;

        isOnIce = isGrounded && hit.collider != null && hit.collider.CompareTag("Slippery");
        float targetSpeed = moveInput * moveSpeed;
        
        if (isOnIce)
        {
            _audioManager.StopContinuousSFX(AudioClipEnum.Walking);
            _audioManager.PlayContinuousSFX(AudioClipEnum.Sliding);

            float iceSpeed = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, Time.fixedDeltaTime * slide);
            rb.linearVelocity = new Vector3(iceSpeed, rb.linearVelocity.y, 0);
        }
        else
        {
            _audioManager.StopContinuousSFX(AudioClipEnum.Sliding);

            if (Mathf.Abs(moveInput) > 0)
            {
                _audioManager.PlayContinuousSFX(AudioClipEnum.Walking);
            }
            else
            {
                _audioManager.StopContinuousSFX(AudioClipEnum.Walking);
            }

            rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.fixedDeltaTime;
        }
    }
}